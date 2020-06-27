CREATE TRIGGER TR_AU_CambioRolAsignarMeritos
ON [dbo].[Miembro]
AFTER UPDATE
AS
DECLARE	@username NVARCHAR(20),
		@nombreRolNuevo NVARCHAR(25),
		@nombreRolViejo NVARCHAR(25),
		@pesoNuevo int,
		@pesoViejo int;
SELECT @username = I.usernamePK FROM inserted I;
SELECT @nombreRolNuevo = I.nombreRolFK FROM inserted I;
SELECT @nombreRolViejo = D.nombreRolFK FROM deleted D;
SELECT @pesoNuevo = peso FROM [dbo].[Rol] WHERE nombrePK = @nombreRolNuevo;
SELECT @pesoViejo = peso FROM [dbo].[Rol] WHERE nombrePK = @nombreRolViejo;
IF UPDATE(nombreRolFK)	
	BEGIN
		UPDATE [dbo].[Miembro]		
		SET meritos += (@pesoNuevo - @pesoViejo)
		WHERE usernamePK = @username;
	END