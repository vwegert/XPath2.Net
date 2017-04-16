using System.Xml;
using Wmhelp.XPath2;
using Xunit;

namespace XPath2.Extensions.Tests
{
    [Collection("xpath2 test collection")]
    public class XPath2ExtensionsTests
    {
        private readonly XPath2TestFixture _fixture;

        public XPath2ExtensionsTests(XPath2TestFixture fixture)
        {
            _fixture = fixture;
        }

        private XmlDocument GetTodoListDoc()
        {
            return new XmlDocument
            {
                InnerXml = @"
                    <todo-list>
                        <todo-item id='a1'>abc</todo-item>
                        <todo-item id='a2'>def</todo-item>
                        <todo-item id='a3'>xyz</todo-item>
                    </todo-list>"
            };
        }

        [Fact]
        public void XPath2_Evaluate_true()
        {
            var nav = GetTodoListDoc().CreateNavigator();
            var result = nav.XPath2Evaluate("boolean(/todo-list[count(todo-item) = 3])");

            Assert.True(true.Equals(result));
        }

        [Fact]
        public void XPath2_Evaluate_false()
        {
            var nav = GetTodoListDoc().CreateNavigator();
            var result = nav.XPath2Evaluate("boolean(/todo-list[count(todo-item) = 4])");

            Assert.False(true.Equals(result));
        }

        [Fact]
        public void XPath2_matches_true()
        {
            var result = _fixture.Navigator.XPath2Evaluate("matches(\"abracadabra\", \"bra\")");

            Assert.Equal(true, result);
        }

        [Fact]
        public void XPath2_matches_IgnoreCase_true()
        {
            var result = _fixture.Navigator.XPath2Evaluate("matches(\"abracadabra\", \"BRa\", \"i\")");

            Assert.Equal(true, result);
        }

        [Fact]
        public void XPath2_matches_false()
        {
            var result = _fixture.Navigator.XPath2Evaluate("matches(\"abracadabra\", \"test\")");

            Assert.Equal(false, result);
        }

        [Fact]
        public void XPath2_substring()
        {
            var result = _fixture.Navigator.XPath2Evaluate("substring(null, 2)");

            Assert.Equal("", result);
        }

        [Fact]
        public void XPathExtensions_base64encode()
        {
            var result = _fixture.Navigator.XPath2Evaluate("base64encode('stef')");

            Assert.Equal("c3RlZg==", result);
        }

        [Fact]
        public void XPathExtensions_base64encode_with_encodings()
        {
            foreach (string e in new[] { "'utf-8'", "'ascii'" })
            {
                var result = _fixture.Navigator.XPath2Evaluate($"base64encode('stef', {e})");

                Assert.Equal("c3RlZg==", result);
            }
        }

        [Fact]
        public void XPathExtensions_base64encode_invalid_encoding()
        {
            var exception = Record.Exception(() => _fixture.Navigator.XPath2Evaluate("base64encode('stef', 'x')"));
            Assert.NotNull(exception);
            Assert.IsType<XPath2Exception>(exception);
            Assert.Equal("The value '\"x\"' is an invalid argument for constructor/cast Encoding.GetEncoding()", exception.Message);
        }

        [Fact]
        public void XPathExtensions_base64decode()
        {
            var result = _fixture.Navigator.XPath2Evaluate("base64decode('c3RlZg==')");

            Assert.Equal("stef", result);
        }

        [Fact]
        public void XPathExtensions_base64decode_with_fixPadding_true()
        {
            foreach (string b in new[] { "'true'", "true()" })
            {
                var result = _fixture.Navigator.XPath2Evaluate($"base64decode('c3RlZg', {b})");

                Assert.Equal("stef", result);
            }
        }

        [Fact]
        public void XPathExtensions_base64decode_with_encodings()
        {
            foreach (string e in new[] { "'utf-8'", "'ascii'" })
            {
                var result = _fixture.Navigator.XPath2Evaluate($"base64decode('c3RlZg==', {e})");

                Assert.Equal("stef", result);
            }
        }

        [Fact]
        public void XPathExtensions_base64decode_with_encoding_and_fixPadding_true()
        {
            foreach (string e in new[] { "utf-8", "ascii" })
            {
                foreach (string b in new[] { "'true'", "true()" })
                {
                    foreach (string str in new[] { "c3RlZg", "=c3RlZg=", "c3RlZg=======" })
                    {
                        var result = _fixture.Navigator.XPath2Evaluate($"base64decode('{str}', '{e}', {b})");

                        Assert.Equal("stef", result);
                    }
                }
            }
        }

        [Fact]
        public void XPathExtensions_base64decode_with_encoding_and_fixPadding_false()
        {
            foreach (string e in new[] { "'utf-8'", "'ascii'" })
            {
                foreach (string b in new[] { "'false'", "false()" })
                {
                    var result = _fixture.Navigator.XPath2Evaluate($"base64decode('c3RlZg==', {e}, {b})");

                    Assert.Equal("stef", result);
                }
            }
        }

        [Fact]
        public void XPathExtensions_base64decode_invalid_data_length()
        {
            var result = _fixture.Navigator.XPath2Evaluate("base64decode('c3RlZg')");

            Assert.Equal("stef", result);
        }

        [Fact]
        public void XPathExtensions_json_to_xml()
        {
            var result = _fixture.Navigator.XPath2Evaluate(@"string(json-to-xml('{ ""id"": 42, ""hello"": ""world"" }', 'r')/r/id)");

            Assert.Equal("42", result);
        }
    }
}