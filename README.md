# InkDemo
WIndows 8.1 app that supports pressure sensitive inking using C# and XAML

## Build
Open InkDemo.sln in Visual Studio 2013 or later and press F5 to build and run the application

## Details

With Windows 8, Microsoft introduced the Inking API in the Windows Runtime. Considering the success of the Surface Pro 3 w/ active stylus, this API is also getting a lot of attention in the  upcoming Windows 10 release to increase it's capabilities and performance.

Most of the samples I've found for the new API use fixed widths for the ink strokes either because they are writing for finger based input or it just completely ignores the pressure input from the stylus.  While it's possible to read the pointer input from the user without handling pressure, the rendered ink isn't providing the tangible experience that users expect when they write with a stylus. In addition, I think that our recognition of handwriting relies  on the nuances of the strokes that only come when we variying the thickness of the ink while stroking a letter or word.
