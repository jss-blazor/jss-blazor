using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using JssBlazor.Components.Models;

namespace JssBlazor.Components.Utilities
{
    public static class MediaApi
    {
        private static readonly Regex ImageTagRegex = new Regex("<img([^>]+)/>", RegexOptions.IgnoreCase);

        private static readonly Regex HtmlAttributesRegex = new Regex("([^=\\s]+)(=\"([^\"]*)\")?", RegexOptions.IgnoreCase);

        private static readonly Regex MediaUrlPrefixRegex = new Regex("/([-~]{1})/media/", RegexOptions.IgnoreCase);

        public static EditorImageTag FindEditorImageTag(string editorMarkup)
        {
            var imageTagMatch = ImageTagRegex.Match(editorMarkup);
            if (imageTagMatch.Length < 2)
            {
                return null;
            }

            var attributes = new Dictionary<string, object>();
            var attributeMatches = HtmlAttributesRegex.Matches(imageTagMatch.Groups[1].Value);
            foreach (Match attributeMatch in attributeMatches)
            {
                var groups = attributeMatch.Groups;
                attributes.Add(groups[1].Value, groups[3].Value);
            }

            return new EditorImageTag
            {
                ImgTag = imageTagMatch.Groups[0].Value,
                Attrs = attributes
            };
        }

        public static string UpdateImageUrl(
            string url,
            ImageSizeParameters parameters = null)
        {
            var parsedUrl = new Uri(url, UriKind.RelativeOrAbsolute);

            var uriBuilder = parsedUrl.IsAbsoluteUri ?
                new UriBuilder(parsedUrl) :
                new UriBuilder("http://www.tempuri.org" + parsedUrl);

            var imageParameters = parameters?.ToString();
            if (!string.IsNullOrWhiteSpace(imageParameters))
            {
                uriBuilder.Query = imageParameters;
            }

            var match = MediaUrlPrefixRegex.Match(uriBuilder.Path);
            if (match.Length > 1)
            {
                // regex will provide us with /-/ or /~/ type
                uriBuilder.Path = MediaUrlPrefixRegex.Replace(uriBuilder.Path, $"/{match.Groups[1].Value}/jssmedia/");
            }

            return parsedUrl.IsAbsoluteUri ?
                uriBuilder.Uri.GetComponents(UriComponents.AbsoluteUri, UriFormat.SafeUnescaped) :
                uriBuilder.Uri.GetComponents(UriComponents.PathAndQuery, UriFormat.SafeUnescaped);
        }

        public static string GetSrcSet(
            string url,
            IEnumerable<ImageSizeParameters> srcSet,
            ImageSizeParameters imageParams = null)
        {
            var srcSetParameters = srcSet.Select(parameters =>
            {
                var newParams = new ImageSizeParameters(imageParams)
                {
                    W = parameters.W,
                    H = parameters.H,
                    Mw = parameters.Mw,
                    Mh = parameters.Mh,
                    Iar = parameters.Iar,
                    As = parameters.As,
                    Sc = parameters.Sc
                };
                var imageWidth = newParams.W ?? newParams.Mw;
                if (imageWidth == null) return null;
                return $"{UpdateImageUrl(url, newParams)} {imageWidth}w";
            }).Where(p => p != null);
            return string.Join(", ", srcSetParameters);
        }
    }
}
