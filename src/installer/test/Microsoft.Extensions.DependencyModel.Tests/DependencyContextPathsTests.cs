﻿// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using Xunit;

namespace Microsoft.Extensions.DependencyModel.Tests
{
    public class DependencyContextPathsTests
    {
        [Fact]
        public void CreateWithNulls()
        {
            var paths = DependencyContextPaths.Create(null, null);

            paths.Application.Should().BeNull();
            paths.SharedRuntime.Should().BeNull();
            paths.ExtraPaths.Should().BeEmpty();
        }

        [Fact]
        public void CreateWithNullFxDeps()
        {
            var paths = DependencyContextPaths.Create("foo.deps.json", null);

            paths.Application.Should().Be("foo.deps.json");
            paths.SharedRuntime.Should().BeNull();
            paths.ExtraPaths.Should().BeEmpty();
        }

        [Fact]
        public void CreateWithDepsFilesContainingFxDeps()
        {
            var paths = DependencyContextPaths.Create("foo.deps.json;fx.deps.json", "fx.deps.json");

            paths.Application.Should().Be("foo.deps.json");
            paths.SharedRuntime.Should().Be("fx.deps.json");
            paths.ExtraPaths.Should().BeEmpty();
        }

        [Fact]
        public void CreateWithExtraContainingFxDeps()
        {
            var paths = DependencyContextPaths.Create(
                "foo.deps.json;fx.deps.json;extra.deps.json;extra2.deps.json", 
                "fx.deps.json");

            paths.Application.Should().Be("foo.deps.json");
            paths.SharedRuntime.Should().Be("fx.deps.json");
            paths.ExtraPaths.Should().BeEquivalentTo("extra.deps.json", "extra2.deps.json");
        }

        [Fact]
        public void CreateWithExtraNotContainingFxDeps()
        {
            var paths = DependencyContextPaths.Create(
                "foo.deps.json;extra.deps.json;extra2.deps.json", 
                "fx.deps.json");

            paths.Application.Should().Be("foo.deps.json");
            paths.SharedRuntime.Should().Be("fx.deps.json");
            paths.ExtraPaths.Should().BeEquivalentTo("extra.deps.json", "extra2.deps.json");
        }
    }
}
