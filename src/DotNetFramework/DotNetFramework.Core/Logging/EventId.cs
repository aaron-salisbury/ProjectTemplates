﻿using System;

namespace DotNetFramework.Core.Logging
{
    /// <summary>
    /// Identifies a logging event. The primary identifier is the "Id" property, with the "Name" property providing a short description of this type of event.
    /// </summary>
    public readonly struct EventId : IEquatable<EventId>
    {
        /// <summary>
        /// Implicitly creates an EventId from the given <see cref="int"/>.
        /// </summary>
        /// <param name="i">The <see cref="int"/> to convert to an EventId.</param>
        public static implicit operator EventId(int i)
        {
            return new EventId(i);
        }

        /// <summary>
        /// Checks if two specified <see cref="EventId"/> instances have the same value. They are equal if they have the same ID.
        /// </summary>
        /// <param name="left">The first <see cref="EventId"/>.</param>
        /// <param name="right">The second <see cref="EventId"/>.</param>
        /// <returns><see langword="true" /> if the objects are equal.</returns>
        public static bool operator ==(EventId left, EventId right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Checks if two specified <see cref="EventId"/> instances have different values.
        /// </summary>
        /// <param name="left">The first <see cref="EventId"/>.</param>
        /// <param name="right">The second <see cref="EventId"/>.</param>
        /// <returns><see langword="true" /> if the objects are not equal.</returns>
        public static bool operator !=(EventId left, EventId right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Initializes an instance of the <see cref="EventId"/> struct.
        /// </summary>
        /// <param name="id">The numeric identifier for this event.</param>
        /// <param name="name">The name of this event.</param>
        public EventId(int id, string name = null)
        {
            Id = id;
            Name = name;
        }

        /// <summary>
        /// Gets the numeric identifier for this event.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Gets the name of this event.
        /// </summary>
        public string Name { get; }

        /// <inheritdoc />
        public override string ToString()
        {
            return Name ?? Id.ToString();
        }

        /// <summary>
        /// Compares the current instance to another object of the same type. Two events are equal if they have the same ID.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns><see langword="true" /> if the current object is equal to <paramref name="other" />; otherwise, <see langword="false" />.</returns>
        public bool Equals(EventId other)
        {
            return Id == other.Id;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            return obj is EventId eventId && Equals(eventId);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return Id;
        }
    }
}
