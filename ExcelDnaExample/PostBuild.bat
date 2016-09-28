COPY /Y ..\..\packages\ExcelDna.AddIn.0.33.9\tools\ExcelDna.xll ExcelDnaExample.xll
COPY /Y ..\..\ExcelDnaExample\ExcelDnaExample.dna .
IF EXIST ExcelDnaExample.xll.config DEL ExcelDnaExample.xll.config
RENAME ExcelDnaExample.dll.config ExcelDnaExample.xll.config