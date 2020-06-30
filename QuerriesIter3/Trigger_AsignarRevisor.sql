CREATE TRIGGER TR_BI_AsignarRevisor
ON [dbo].[NucleoRevisaArticulo]
FOR INSERT
AS
DECLARE @articuloAID INT,
		@usernameMiemFK NVARCHAR(50)
SELECT @usernameMiemFK = I.usernameMiemFK FROM inserted I
SELECT @articuloAID = I.idArticuloFK FROM inserted I

BEGIN
		DELETE FROM [NucleoPuedeSerRevisorDeArticulo]
		WHERE @usernameMiemFK = [dbo].[NucleoPuedeSerRevisorDeArticulo].usernameMiemFK AND
         @articuloAID = [dbo].[NucleoPuedeSerRevisorDeArticulo].idArticuloFK
END