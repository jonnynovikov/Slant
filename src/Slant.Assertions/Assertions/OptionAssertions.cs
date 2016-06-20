using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using LanguageExt;

namespace Slant.Assertions
{
    public class OptionAssertions<T> : ReferenceTypeAssertions<Option<T>, OptionAssertions<T>>
    {
        protected override string Context => $"Option<{typeof(T).Name}>";

        public OptionAssertions(Option<T> opt)
        {
            Subject = opt;
        }

        /// <summary>
        /// Asserts that the current object is None.
        /// </summary>
        /// <param name="because">
        /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion 
        /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="becauseArgs">
        /// Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndConstraint<OptionAssertions<T>> BeNone(string because = "", params object[] becauseArgs)
        {
            Execute.Assertion
                .ForCondition(Subject.IsNone)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context:" + Context + "} to be None{reason}, but found {0}.", Subject);

            return new AndConstraint<OptionAssertions<T>>(this);
        }

        /// <summary>
        /// Asserts that the current object is Some.
        /// </summary>
        /// <param name="because">
        /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion 
        /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="becauseArgs">
        /// Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndConstraint<OptionAssertions<T>> BeSome(string because = "", params object[] becauseArgs)
        {
            Execute.Assertion
                .ForCondition(Subject.IsSome)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context:" + Context + "} to be Some{reason}, but found {0}.", Subject);

            return new AndConstraint<OptionAssertions<T>>(this);
        }
    }
}