﻿// using DIKUArcade;
// using DIKUArcade.Entities;
// using DIKUArcade.Graphics;
// using OpenTK.Windowing.Common;
// using OpenTK.Windowing.GraphicsLibraryFramework;

// namespace TestDIKUArcade {
//     public class TestAnimationContainer {
//         public static void MainFunction() {
//             var win = new DIKUArcade.Window("TestAnimationContainer", 500, AspectRatio.R1X1);

//             var container = new AnimationContainer(4);
//             var strides = ImageStride.CreateStrides(4, "PuffOfSmoke.png");

//             win.AddKeyPressEventHandler(delegate(KeyboardKeyEventArgs keyArgs) {
//                 switch (keyArgs.Key) {
//                 case Keys.Escape:
//                     win.CloseWindow();
//                     break;
//                 case Keys.D1:
//                     container.AddAnimation(new StationaryShape(0.0f, 0.0f, 0.5f, 0.5f), 1000,
//                         new ImageStride(80, strides));
//                     break;
//                 case Keys.D2:
//                     container.AddAnimation(new StationaryShape(0.5f, 0.0f, 0.5f, 0.5f), 1000,
//                         new ImageStride(80, strides));
//                     break;
//                 case Keys.D3:
//                     container.AddAnimation(new StationaryShape(0.0f, 0.5f, 0.5f, 0.5f), 1000,
//                         new ImageStride(80, strides));
//                     break;
//                 case Keys.D4:
//                     container.AddAnimation(new StationaryShape(0.5f, 0.5f, 0.5f, 0.5f), 1000,
//                         new ImageStride(80, strides));
//                     break;
//                 }
//             });

//             while (win.IsRunning()) {
//                 win.PollEvents();
//                 win.Clear();
//                 container.RenderAnimations();
//                 win.SwapBuffers();
//             }
//         }
//     }
// }
