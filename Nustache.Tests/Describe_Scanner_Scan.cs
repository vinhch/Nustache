﻿using System.Linq;
using NUnit.Framework;
using Nustache.Core;

namespace Nustache.Tests
{
    [TestFixture]
    public class Describe_Scanner_Scan
    {
        [Test]
        public void It_returns_no_parts_for_the_empty_string()
        {
            var scanner = new Scanner();

            var parts = scanner.Scan("");

            CollectionAssert.AreEqual(new Part[]
                                      {
                                      },
                                      parts.ToArray());
        }

        [Test]
        public void It_scans_literal_text()
        {
            var scanner = new Scanner();

            var parts = scanner.Scan("foo");

            CollectionAssert.AreEqual(new Part[]
                                      {
                                          new LiteralText("foo"),
                                      },
                                      parts.ToArray());
        }

        [Test]
        public void It_scans_variable_markers()
        {
            var scanner = new Scanner();

            var parts = scanner.Scan("before{{foo}}after");

            CollectionAssert.AreEqual(new Part[]
                                      {
                                          new LiteralText("before"),
                                          new VariableMarker("foo"),
                                          new LiteralText("after"),
                                      },
                                      parts.ToArray());
        }

        [Test]
        public void It_scans_sections()
        {
            var scanner = new Scanner();

            var parts = scanner.Scan("{{#foo}}inside{{/foo}}");

            CollectionAssert.AreEqual(new Part[]
                                      {
                                          new Block("foo"),
                                          new LiteralText("inside"),
                                          new EndSection("foo"),
                                      },
                                      parts.ToArray());
        }

        [Test]
        public void It_scans_template_include_markers()
        {
            var scanner = new Scanner();

            var parts = scanner.Scan("before{{>foo}}after");

            CollectionAssert.AreEqual(new Part[]
                                      {
                                          new LiteralText("before"),
                                          new TemplateInclude("foo"),
                                          new LiteralText("after"),
                                      },
                                      parts.ToArray());
        }

        [Test]
        public void It_skips_comment_markers()
        {
            var scanner = new Scanner();

            var parts = scanner.Scan("before{{!foo}}after");

            CollectionAssert.AreEqual(new Part[]
                                      {
                                          new LiteralText("before"),
                                          new LiteralText("after"),
                                      },
                                      parts.ToArray());
        }
    }
}