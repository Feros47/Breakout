namespace BreakoutTests.Stubs;
using System;

public class StubException : Exception {
    public StubException() {
    }
    public StubException(string msg) : base(msg) {
    }
}
