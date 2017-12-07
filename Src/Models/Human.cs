using System;
using System.Collections.Generic;
using System.Text;

namespace WikiDataNameGuesser.Models
{
    public class Human
    {
        /// <summary>
        /// Wikidata Id
        /// </summary>
        public string Id;

        /// <summary>
        /// Wikidata label
        /// </summary>
        public string Label;

        /// <summary>
        /// Birth name string
        /// </summary>
        public string BirthName;

        /// <summary>
        /// FamilyName label
        /// </summary>
        public string FamilyName;
        /// <summary>
        /// FamilyName WikiData Id
        /// </summary>
        public string FamilyNameId;

        /// <summary>
        /// GivenName label
        /// </summary>
        public string GivenName;
        /// <summary>
        /// GivenNameId WikiData Id
        /// </summary>
        public string GivenNameId;
    }
}
