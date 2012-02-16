using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SqlDatabase.Entities;
using SqlDatabase.Model;
using System.Data.Objects;
using System.Data;

namespace SqlDatabase
{
    public class DatabaseInterface
    {
        #region Entities
        // ----------------------------------------------------------------------------------------
        private EntityDataModelContainer Entities;

        /// <summary>
        /// Return All entities
        /// </summary>
        /// <returns>Entity Data Model Container</returns>
        public EntityDataModelContainer GetEntities()
        {
            try
            {
                ConnectionState ES = Entities.Connection.State;
                return Entities;
            }
            catch (Exception e)
            {
                AddError(e);
            }
            return Entities;
        }

        /// <summary>
        /// Return Article entities
        /// </summary>
        public ObjectSet<Article> Articles
        {
            get
            {
                if (GetEntities() == null)
                    return null;
                else
                    return GetEntities().Articles;
            }
        }

        /// <summary>
        /// Return Comment entities
        /// </summary>
        public ObjectSet<Comment> Comments
        {
            get
            {
                if (GetEntities() == null)
                    return null;
                else
                    return GetEntities().Comments;
            }
        }

        /// <summary>
        /// Return Credentails entities
        /// </summary>
        public ObjectSet<Credentail> Credentails
        {
            get
            {
                if (GetEntities() == null)
                    return null;
                else
                    return GetEntities().Credentails;
            }
        }

        /// <summary>
        /// Return Group entities
        /// </summary>
        public ObjectSet<Group> Groups
        {
            get
            {
                if (GetEntities() == null)
                    return null;
                else
                    return GetEntities().Groups;
            }
        }

        /// <summary>
        /// Return Option entities
        /// </summary>
        public ObjectSet<Option> Options
        {
            get
            {
                if (GetEntities() == null)
                    return null;
                else
                    return GetEntities().Options;
            }
        }

        /// <summary>
        /// Return OptionType entities
        /// </summary>
        public ObjectSet<OptionType> OptionTypes
        {
            get
            {
                if (GetEntities() == null)
                    return null;
                else
                    return GetEntities().OptionTypes;
            }
        }

        /// <summary>
        /// Return Report entities
        /// </summary>
        public ObjectSet<Report> Reports
        {
            get
            {
                if (GetEntities() == null)
                    return null;
                else
                    return GetEntities().Reports;
            }
        }

        /// <summary>
        /// Return Tag entities
        /// </summary>
        public ObjectSet<Tag> Tags
        {
            get
            {
                if (GetEntities() == null)
                    return null;
                else
                    return GetEntities().Tags;
            }
        }

        /// <summary>
        /// Return User entities
        /// </summary>
        public ObjectSet<User> Users
        {
            get
            {
                if (GetEntities() == null)
                    return null;
                else
                    return GetEntities().Users;
            }
        }

        // ----------------------------------------------------------------------------------------
        #endregion

        #region Errors
        // ----------------------------------------------------------------------------------------
        private List<Exception> ErrorStack;

        /// <summary>
        /// Add an error into the error stack
        /// </summary>
        /// <param name="Exception"></param>
        private void AddError(Exception Exception)
        {
            CreateErrorStackIfNotExist();
            ErrorStack.Add(Exception);
        }

        /// <summary>
        /// Return the list of error stack
        /// </summary>
        /// <returns></returns>
        public List<Exception> GetErrors()
        {
            CreateErrorStackIfNotExist();
            return ErrorStack;
        }

        /// <summary>
        /// Create Error Stack
        /// </summary>
        public void ResetErrorStack()
        {
            ErrorStack = new List<Exception>();
        }

        /// <summary>
        /// Create error stack if unavailable
        /// </summary>
        private void CreateErrorStackIfNotExist()
        {
            if (ErrorStack == null)
                ResetErrorStack();
        }

        // ----------------------------------------------------------------------------------------
        #endregion

        #region Constructor
        // ----------------------------------------------------------------------------------------
        /// <summary>
        /// Constructor
        /// </summary>
        public DatabaseInterface()
        {
            ResetErrorStack();
            Entities = new EntityDataModelContainer();
        }
        // ----------------------------------------------------------------------------------------
        #endregion

        #region Database Management
        // ----------------------------------------------------------------------------------------
        public bool IsAvailable()
        {
            return Entities.Connection.State == ConnectionState.Open;
        }
        // ----------------------------------------------------------------------------------------
        #endregion

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
