using System;
using System.Runtime.CompilerServices;
using Tessa.Platform;

namespace Tessa.Extensions.Default.Server.Workflow.KrObjectModel
{
    public class Author :
        IEquatable<Author>,
        ISealable
    {
        #region constructor

        protected Author()
        {
        }
        
        public Author(
            Guid roleID,
            string roleName)
        {
            this.AuthorID = roleID;
            this.AuthorName = roleName;
        }

        #endregion

        #region properties

        /// <summary>
        /// ID автора
        /// </summary>
        public virtual Guid AuthorID { get; private set; }

        /// <summary>
        /// Имя автора
        /// </summary>
        public virtual string AuthorName { get; private set; }

        #endregion

        #region public

        public static bool operator ==(Author left, Author right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Author left, Author right)
        {
            return !Equals(left, right);
        }
        
        public override string ToString()
        {
            return $"Author: ID = {this.AuthorID:B}, Name = {this.AuthorName}";
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            return obj is Author author && this.Equals(author);
        }

        /// <inheritdoc />
        public override int GetHashCode() => RuntimeHelpers.GetHashCode(this);

        /// <inheritdoc />
        public bool Equals(Author other) => other != null && this.AuthorID == other.AuthorID;

        /// <inheritdoc />
        public bool IsSealed { get; } = true;

        /// <inheritdoc />
        public void Seal()
        {
        }
        
        #endregion

    }
}