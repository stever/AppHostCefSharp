# AppHostCefSharp

## About

CefSharp doesn't play well with App Domains, and so doesn't work easily using
Excel-DNA, for example. It had been suggested that RedGate.AppHost could be used
to deal with the problem, and so this example project was made. It works!

## Build

This solution was built using Visual Studio 2019.

Usual procedure to launch an Excel add-in:

  1. Open the Properties window for the ExcelDnaExample project.
  2. Set the 'Start external program' option to 'EXCEL.EXE' in path for your Office install.
  3. Set the 'Command line arguments' option to 'ExcelDnaExample.xll'

Now when you Start the solution it will launch Excel and the add-in.

## Acknowledgements

Thanks to the following:

  * @jornh for the RedGate.AppHost suggestion.
  * @arsher for the appdomainsafe branch which I have been using.
  * @AmaelN for an interesting example using WPF with Excel-DNA.
  * RedGate for the AppHost library.
  * All the team making CefSharp for that neat project.

## Note

There is an alternative branch called `named-pipe` which doesn't seem to work currently, 
but may be useful to consider as an alternative to the embedded HTTP server example here.