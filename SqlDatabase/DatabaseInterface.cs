using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SqlDatabase.Entities;
using SqlDatabase.Model;

namespace SqlDatabase
{
    public class DatabaseInterface
    {
        private EntityDataModelContainer Entities;

        public DatabaseInterface()
        {
            Entities = new EntityDataModelContainer();
        }

        public object GetOption(string OptionName, object DefaultValue)
        {
            Option Option = (from r in Entities.Options where r.Name == OptionName select r).FirstOrDefault();
            if (Option == null)
            {
                return DefaultValue;
            }
            else
            {
                switch (Option.OptionType.Name)
                {
                    case "Boolean": return Option.Boolean;
                    case "DateTime": return Option.DateTime;
                    case "Decimal": return Option.Decimal;
                    case "Integer": return Option.Integer;
                    case "String": return Option.String;
                }
                return DefaultValue;
            }
        }
        private OptionType GetOptionType(string OptionTypeName)
        {
            OptionType OptionType = (from r in Entities.OptionTypes where r.Name == OptionTypeName select r).FirstOrDefault();
            if (OptionType != null)
                return OptionType;
            OptionType NewOptionType = new OptionType();
            NewOptionType.Name = OptionTypeName;
            Entities.OptionTypes.AddObject(NewOptionType);
            Entities.SaveChanges();
            return GetOptionType(OptionTypeName);
        }

        public List<Article> GetHighlightedArticles()
        {
            return (from r in Entities.Articles where r.Published == true && r.Highlight == true orderby r.CreatedAt descending select r).Take((int)GetOption("NumberOfHighlightedArticles", 5)).ToList();
        }
    }
}
