
DECLARE @startDate datetime = DATEADD(dd, -7, GETDATE())
DECLARE @tempSymbol nchar(10)
DECLARE @avgVolume bigint
DECLARE @thresholdVolume bigint

IF EXISTS (SELECT OBJECT_ID('#AllStocks'))
BEGIN
	DROP TABLE #AllStocks
END

CREATE TABLE #AllStocks
(Symbol nchar(10))

IF EXISTS (SELECT OBJECT_ID('##StocksOfInterest'))
BEGIN
	DROP TABLE ##StocksOfInterest
END

CREATE TABLE ##StocksOfInterest
(Symbol nchar(10),
	ShortDate Datetime,
	[AvgVolume] BigInt,
	[Volume] BigInt,
	[PercentShort] Float)

INSERT INTO #AllStocks
	SELECT Symbol from dbo.ShortEntry WHERE ShortDate >= @startDate GROUP by Symbol

WHILE EXISTS (SELECT TOP 1 * FROM #AllStocks)
BEGIN
	SET @tempSymbol = (SELECT TOP 1 [Symbol] FROM #AllStocks)
	SET @avgVolume = (SELECT AVG(TotalVolume) FROM [dbo].[ShortEntry] WHERE [Symbol] = @tempSymbol)
	SET @thresholdVolume = @avgVolume * 3.5
	Print N'Working on: ' + @tempSymbol

	INSERT INTO ##StocksOfInterest ([Symbol], [ShortDate], [AvgVolume], [Volume], [PercentShort])
	SELECT [Symbol], [ShortDate], @avgVolume, [TotalVolume], [PercentShort]
		FROM [dbo].[ShortEntry]
		WHERE [Symbol] = @tempSymbol 
			AND [ShortDate] >= @startDate
			AND [TotalVolume] >= @thresholdVolume

	DELETE FROM #AllStocks
		WHERE [Symbol] = @tempSymbol
END


SELECT * from ##StocksOfInterest
	WHERE [PercentShort] < .45
		AND [Symbol] IN (SELECT [Symbol] FROM ##StocksOfInterest GROUP BY [Symbol] HAVING COUNT(*) >= 3)
		AND [Volume] > (5 * [AvgVolume])
ORDER BY [Symbol] ASC

