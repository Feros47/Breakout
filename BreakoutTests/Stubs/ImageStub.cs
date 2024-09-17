namespace BreakoutTests.Stubs;

using System;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;

public class ImageStub : IBaseImage {
    public void Render(Shape shape) {
        throw new StubException($"{nameof(ImageStub)}.{nameof(Render)}");
    }
    public void Render(Shape shape, Camera camera) {
        throw new NotImplementedException();
    }
}
