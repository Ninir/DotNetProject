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
            return false;
            /*
            Option Option = (from r in Entities.Options where r.Name == OptionName select r).FirstOrDefault();
            if (Option == null)
            {
                Func<KeyValuePair<string,object>,bool> Predicat = o => o.Key == OptionName;
                KeyValuePair<string,object> DefaultOption = Configuration.DefaultOptions.Where(o => o.Key == OptionName).FirstOrDefault();
                if(Configuration.DefaultOptions.Count(Predicat) > 0)
                {
                    return DefaultValue;
                }


                Option NewOption = new Option();
                string DefaultType = DefaultValue.GetType().Name;
                if (DefaultValue is DateTime)
                {
                    NewOption.OptionType = GetOptionType(Configuration.DefaultOptionType.DateTime);
                    NewOption.DateTime = (DateTime)DefaultValue;
                }
                else if (DefaultValue is bool)
                {
                    NewOption.OptionType = GetOptionType(Configuration.DefaultOptionType.Boolean);
                    NewOption.Boolean = (bool)DefaultValue;
                }
                else if (DefaultValue is decimal)
                {
                    NewOption.OptionType = GetOptionType(Configuration.DefaultOptionType.Decimal);
                    NewOption.Decimal = (decimal)DefaultValue;
                }
                else if (DefaultValue is int)
                {
                    NewOption.OptionType = GetOptionType(Configuration.DefaultOptionType.Integer);
                    NewOption.Integer = (int)DefaultValue;
                }
                else if (DefaultValue is string)
                {
                    NewOption.OptionType = GetOptionType(Configuration.DefaultOptionType.String);
                    NewOption.String = (string)DefaultValue;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                switch (Option.OptionType.Name)
                {
                    case Configuration.DefaultOptionType.Boolean:  return Option.Boolean;
                    case Configuration.DefaultOptionType.DateTime: return Option.DateTime;
                    case Configuration.DefaultOptionType.Decimal: return Option.Decimal;
                    case Configuration.DefaultOptionType.Integer: return Option.Integer;
                    case Configuration.DefaultOptionType.String:   return Option.String;
                }
            }
             */
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

        public List<Article> GetHighlight()
        {
            return null;
            //return (from r in Entities.Articles where r.Published == true && r.Highlight == true orderby r.CreatedAt descending select r).Take(Configuration.DefaultOptions["NumberOfHighlightedArticles"].Value).ToList();
        }
    }
}
