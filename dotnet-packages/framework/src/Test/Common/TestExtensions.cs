using FluentAssertions;
using FluentAssertions.Collections;
using FluentAssertions.Equivalency;
using FluentAssertions.Primitives;
using Framework.Core.Bus;
using Framework.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Framework.Test.Common
{
    public static class TestExtensions
    {
        #region ObjectAssertions

        public static void BeEquivalentToMessage<T>(this ObjectAssertions assertations, T expectation, Func<EquivalencyAssertionOptions<T>, EquivalencyAssertionOptions<T>> config = null)
            where T : class, IBusMessage
        {
            BeEquivalentToModel(assertations, expectation, x =>
            {
                x.Excluding(y => y.MessageId)
                 .Excluding(y => y.RequestId);

                if (config != null)
                    config.Invoke(x);

                return x;
            });
        }

        public static void BeEquivalentToEntity<T>(this ObjectAssertions assertations, T expectation, Func<EquivalencyAssertionOptions<T>, EquivalencyAssertionOptions<T>> config = null)
           where T : class, IEntity
        {
            BeEquivalentToModel(assertations, expectation, x =>
            {
                x.Excluding(y => y.SelectedMemberPath.Contains("InsertedAt"));
                x.Excluding(y => y.SelectedMemberPath.Contains("UpdatedAt"));
                x.Excluding(y => y.SelectedMemberPath.Contains("DeletedAt"));
                x.Excluding(y => Regex.IsMatch(y.SelectedMemberPath, "Profilings\\[.+\\].Id"));
                x.IgnoringCyclicReferences();

                if (config != null)
                    config.Invoke(x);

                return x;
            });
        }

        public static void BeEquivalentToModel<T>(this ObjectAssertions assertations, T expectation, Func<EquivalencyAssertionOptions<T>, EquivalencyAssertionOptions<T>> config = null)
        {
            assertations.BeEquivalentTo(expectation, x =>
            {
                x.Using<DateTime>(y => y.Subject.Should().BeCloseTo(y.Expectation, 5000)).WhenTypeIs<DateTime>();
                x.Using<DateTime?>(y =>
                {
                    if (y.Expectation.HasValue)
                        y.Subject.Should().BeCloseTo(y.Expectation.Value, 5000);
                    else
                        y.Subject.Should().Be(y.Expectation);
                }).WhenTypeIs<DateTime?>();

                if (config != null)
                    config.Invoke(x);

                return x;
            });
        }

        #endregion ObjectAssertions

        #region CollectionAssertions

        public static void BeEquivalentToMessage<T>(this GenericCollectionAssertions<T> assertations, IEnumerable<T> expectation, Func<EquivalencyAssertionOptions<T>, EquivalencyAssertionOptions<T>> config = null)
          where T : class, IBusMessage
        {
            BeEquivalentToModel(assertations, expectation, x =>
            {
                x.Excluding(y => y.MessageId)
                  .Excluding(y => y.RequestId);

                if (config != null)
                    config.Invoke(x);

                return x;
            });
        }

        public static void BeEquivalentToEntity<T>(this GenericCollectionAssertions<T> assertations, IEnumerable<T> expectation, Func<EquivalencyAssertionOptions<T>, EquivalencyAssertionOptions<T>> config = null)
          where T : class, IEntity
        {
            BeEquivalentToModel(assertations, expectation, x =>
            {
                x.Excluding(y => y.SelectedMemberPath.Contains("InsertedAt"));
                x.Excluding(y => y.SelectedMemberPath.Contains("UpdatedAt"));
                x.Excluding(y => y.SelectedMemberPath.Contains("DeletedAt"));
                x.Excluding(y => Regex.IsMatch(y.SelectedMemberPath, "Profilings\\[.+\\].Id"));
                x.IgnoringCyclicReferences();

                if (config != null)
                    config.Invoke(x);

                return x;
            });
        }

        public static void BeEquivalentToModel<T>(this GenericCollectionAssertions<T> assertations, IEnumerable<T> expectation, Func<EquivalencyAssertionOptions<T>, EquivalencyAssertionOptions<T>> config = null)
        {
            assertations.BeEquivalentTo(expectation, x =>
            {
                x.Using<DateTime>(y => y.Subject.Should().BeCloseTo(y.Expectation, 5000)).WhenTypeIs<DateTime>();
                x.Using<DateTime?>(y =>
                {
                    if (y.Expectation.HasValue)
                        y.Subject.Should().BeCloseTo(y.Expectation.Value, 5000);
                    else
                        y.Subject.Should().Be(y.Expectation);
                }).WhenTypeIs<DateTime?>();

                if (config != null)
                    config.Invoke(x);

                return x;
            });
        }

        #endregion CollectionAssertions
    }
}