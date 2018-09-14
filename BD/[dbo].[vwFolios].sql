USE [Enercable]
GO

/****** Object:  View [dbo].[vwFolios]    Script Date: 14/09/2018 01:33:43 a.m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vwFolios]
AS
SELECT        P.IdFolio, P.Folio, P.Ciudad, P.Estado, P.Cluster, P.OLT, P.ClientesAfectados, P.FallaReportada, CAST(P.FechaYHoraDeAsignacionDespacho AS varchar) 
                         AS FechaYHoraDeAsignacionDespacho, CAST(P.FechaYHoraDeAsignacionProvista AS varchar) AS FechaYHoraDeAsignacionProvista, 
                         CAST(P.HoraDeLlegadaALaZona AS varchar) AS HoraDeLlegadaALaZona, P.HoraDeLaPrimeraMedicion, 
                         P.UbicacionDePrimerSegundoNivelYDerivacionConSusFibras, P.CausaDelDano, P.UbicacionDelDano, P.CoordenadasDelDanoX, P.CoordenadasDelDanoY, 
                         P.DescripcionDeActividades, P.PotencialInicia, P.PotencialFinal, CAST(P.FechaHoraFinalReparacion AS varchar) AS FechaHoraFinalReparacion, 
                         P.IdPersona_TecnicoAtiende, P.IdProveedor, P.IdPersona_Supervisor, P.IdPersona_AtendioDespacho, P.JustificacionDeSLA, P.FechaSistema, P.IdSesion, 
                         Tecnico.Paterno + ' ' + Tecnico.Materno + ' ' + Tecnico.Nombre AS Tecnico, Supervisor.Paterno + ' ' + Supervisor.Materno + ' ' + Supervisor.Nombre AS Supervisor, 
                         Atendio.Paterno + ' ' + Atendio.Materno + ' ' + Atendio.Nombre AS Atendio, Proveedor.Proveedor, ISNULL(P.IdDepartamento, 0) AS IdDepartamento, ISNULL
                             ((SELECT        Departamento
                                 FROM            dbo.Departamentos AS dep
                                 WHERE        (IdDepartamento = P.IdDepartamento)), '') AS Departamento, ISNULL(P.IdFormato, 0) AS IdFormato, ISNULL
                             ((SELECT        Formato
                                 FROM            dbo.Formatos AS Form
                                 WHERE        (IdFormato = P.IdFormato)), '') AS Formato
FROM            dbo.Folios AS P INNER JOIN
                         dbo.Personas AS Tecnico ON P.IdPersona_TecnicoAtiende = Tecnico.IdPersona INNER JOIN
                         dbo.Personas AS Supervisor ON P.IdPersona_Supervisor = Supervisor.IdPersona INNER JOIN
                         dbo.Personas AS Atendio ON P.IdPersona_AtendioDespacho = Atendio.IdPersona INNER JOIN
                         dbo.Proveedores AS Proveedor ON P.IdProveedor = Proveedor.IdProveedor
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[4] 2[31] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "P"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 125
               Right = 401
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Tecnico"
            Begin Extent = 
               Top = 6
               Left = 675
               Bottom = 125
               Right = 873
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Supervisor"
            Begin Extent = 
               Top = 224
               Left = 636
               Bottom = 343
               Right = 834
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Atendio"
            Begin Extent = 
               Top = 6
               Left = 911
               Bottom = 125
               Right = 1109
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Proveedor"
            Begin Extent = 
               Top = 6
               Left = 1147
               Bottom = 125
               Right = 1345
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 33
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width =' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwFolios'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N' 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwFolios'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwFolios'
GO


