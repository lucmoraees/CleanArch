using CleanArch.Domain.Entities;
using CleanArch.Domain.Validation;
using FluentAssertions;
using Xunit;

namespace CleanArch.Domain.Tests
{
    public class CategoryUnitTest1
    {
        [Fact]
        public void CreateCategory_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => new Category(1, "Category Name");

            action.Should().NotThrow<DomainExceptionValidation>();
        }

        [Fact]
        public void CreateCategory_WithNegativeIdValue_DomainExceptionInvalidId()
        {
            Action action = () => new Category(-1, "Category Name");

            action.Should().Throw<DomainExceptionValidation>();
        }

        [Fact]
        public void CreateCategory_ShortNameValue_DomainExceptionInvalidId()
        {
            Action action = () => new Category(1, "AA");

            action.Should().Throw<DomainExceptionValidation>();
        }

        [Fact]
        public void CreateCategory_MissingNameValue_DomainExceptionInvalidId()
        {
            Action action = () => new Category(1, "");

            action.Should().Throw<DomainExceptionValidation>();
        }

        [Fact]
        public void CreateCategory_WhitNullNameValue_DomainExceptionInvalidId()
        {
            Action action = () => new Category(1, null);

            action.Should().Throw<DomainExceptionValidation>();
        }
    }
}
