CREATE PROCEDURE util.sp_get_increment_by_1
AS 
BEGIN
    SELECT NEXT VALUE FOR util.seq_increment_by_1;
END