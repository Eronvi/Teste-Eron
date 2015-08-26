CREATE PROCEDURE P_RETORNA_IMPOSTOS
AS

SELECT 
CFOP, 
SUM(BaseIcms) AS [Valor Total da Base de ICMS], 
SUM(ValorIcms)  AS [Valor Total do ICMS], 
SUM(BaseCalculoIPI) AS [Valor Total da Base de IPI], 
SUM([ValorIPI]) AS [Valor Total do IPI]
FROM [dbo].[NotaFiscalItem] NOLOCK
GROUP BY CFOP

