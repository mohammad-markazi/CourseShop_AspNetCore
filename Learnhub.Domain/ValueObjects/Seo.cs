using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Learnhub.Domain.ValueObjects
{
    public class Seo:ValueObject
    {
        public string Slug { get; private set; }
        public string Keywords { get; private set; }
        public string MetaDescription { get; private set; }

        public Seo(string slug, string keywords, string metaDescription)
        {
            if(string.IsNullOrEmpty(slug))
                throw new ArgumentNullException(nameof(slug),"cat not be null field");

            if (string.IsNullOrEmpty(keywords))
                throw new ArgumentNullException(nameof(keywords), "cat not be null field");

            if (string.IsNullOrEmpty(metaDescription))
                throw new ArgumentNullException(nameof(metaDescription), "cat not be null field");

            if (keywords.Contains("-") == false)
                throw new InvalidDataException("keywords not valid input");


            Slug = slug;
            Keywords = keywords;
            MetaDescription = metaDescription;
        }

        public Seo GetSeoFormat()
        {
            Slug = Slugify(Slug);

            return new Seo(Slug, Keywords, MetaDescription);
        }

        private string Slugify(string phrase)
        {
            var s = RemoveDiacritics(phrase);
            s = Regex.Replace(s, @"[^\u0600-\u06FF\uFB8A\u067E\u0686\u06AF\u200C\u200Fa-z0-9\s-]",
                ""); // remove invalid characters
            s = Regex.Replace(s, @"\s+", " ").Trim(); // single space
            s = s.Substring(0, s.Length <= 100 ? s.Length : 45).Trim(); // cut and trim
            s = Regex.Replace(s, @"\s", "-"); // insert hyphens        
            s = Regex.Replace(s, @"‌", "-"); // half space
            return s.ToLower();
        }

        private string RemoveDiacritics(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            var normalizedString = text.Normalize(NormalizationForm.FormKC);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC).ToLower();
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Slug;
        }
    }
}
