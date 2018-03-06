using System.Collections;

namespace SqlReflect
{
    public interface IDataMapper
    {
        /// <summary>
        /// Returns a domain object with the given id.
        /// </summary>
        object GetById(object id);
        /// <summary>
        /// Returns all rows as domain objects from the corresponding table.
        /// </summary>
        IEnumerable GetAll();
        /// <summary>
        /// Inserts the target domain object into the corresponding table.
        /// </summary>
        /// <returns>The identity value of the primary key column.</returns>
        object Insert(object target);
        /// <summary>
        /// Updates the corresponding table row with the values of the target domain object.
        /// </summary>
        void Update(object target);
        /// <summary>
        /// Removes the row of the table corresponding to the target domain object.
        /// </summary>
        void Delete(object target);
    }
}
