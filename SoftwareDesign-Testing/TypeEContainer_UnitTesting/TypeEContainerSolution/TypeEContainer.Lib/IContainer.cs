namespace Container.Lib
{
    /// <summary>
    /// A TypeEContainer is a generic container of a
    /// generic type.
    /// </summary>
    /// <remarks>
    /// The TypeEContainer must have a set size. Can
    /// have duplicate values.
    /// </remarks>
    public interface IContainer<in TE>
    {
        /// <summary>
        /// Insert adds an item to the end of the list.
        /// </summary>
        /// <remarks>
        /// Pre-condition:
        /// 1. A container Exists.
        /// 2. A container has a set size.
        /// 3. Verify it has capacity.
        /// Post-condition:
        /// 1. Count is incremented by one.
        /// 2. Data is added to back of container.
        /// Invariant:
        /// 1. Container is Full - should throw exception
        /// 2. E is "too big" - should throw exception
        /// 3. Item given is non-type E - should throw exception
        /// 4. Item given is NULL - should throw exception
        /// </remarks>
        /// <returns>The value of <paramref name="arg" /> </returns>
        /// <param name="e">The item to insert</param>
        void Insert(TE e);
        /// <summary>
        /// Delete removes an item from the container if
        /// it exists within it
        /// </summary>
        /// <remarks>
        /// Pre-condition:
        /// 1. A container Exists.
        /// 2. Container is non-empty
        /// Post-condition:
        /// 1. Count is decremented by one.
        /// 2. The item is removed from the container
        /// 3. Multiple instances should be removed
        /// Invariant:
        /// 1. Container is empty - should throw exception
        /// 2. E is "too big" - should throw exception
        /// 3. Item given is non-type E - should throw exception
        /// 4. Item given is NULL - should throw exception
        /// 5. Item is not in container - should throw exception
        /// </remarks>
        /// <returns>The value of <paramref name="arg" /> </returns>
        /// <param name="e">The item to delete</param>
        void Delete(TE e);
        /// <summary>
        /// Exists returns true if item is in container,
        /// and false otherwise
        /// </summary>
        /// <remarks>
        /// Pre-condition:
        /// 1. A container exists
        /// Post-condition:
        /// 1. Item in container - returns true
        /// 2. Empty container - returns false
        /// 3. Item is not in container - returns false
        /// 4. Non-type e given - returns false
        /// Invariant:
        /// 1. Item given is NULL - should throw exception
        /// </remarks>
        /// <returns>The value of <paramref name="arg" /> </returns>
        /// <param name="e">The item to check if exists in container</param>
        bool Exists(TE e);
    }
}