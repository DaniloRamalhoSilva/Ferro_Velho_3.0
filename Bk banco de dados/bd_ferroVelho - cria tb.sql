USE [bd_ferroVelho]
GO
/****** Object:  Table [dbo].[tb_caixa]    Script Date: 29/07/2021 10:30:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_caixa](
	[Id_caixa] [int] IDENTITY(1,1) NOT NULL,
	[data_caixa] [datetime] NOT NULL,
	[valor_caixa] [decimal](18, 2) NOT NULL,
	[usuario] [int] NULL,
 CONSTRAINT [PK_tb_caixa] PRIMARY KEY CLUSTERED 
(
	[Id_caixa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tb_compra]    Script Date: 29/07/2021 10:30:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_compra](
	[id_compra] [int] IDENTITY(1,1) NOT NULL,
	[data_compra] [datetime] NOT NULL,
	[valor_nota] [decimal](18, 2) NULL,
	[usuario] [int] NULL,
 CONSTRAINT [PK_tb_venda] PRIMARY KEY CLUSTERED 
(
	[id_compra] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tb_comprar]    Script Date: 29/07/2021 10:30:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_comprar](
	[id_comprar] [int] IDENTITY(1,1) NOT NULL,
	[data_compra] [datetime] NOT NULL,
	[valor_nota] [decimal](18, 2) NULL,
	[usuario] [int] NOT NULL,
 CONSTRAINT [PK_tb_comprar] PRIMARY KEY CLUSTERED 
(
	[id_comprar] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tb_impressora]    Script Date: 29/07/2021 10:30:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_impressora](
	[id_impressora] [int] IDENTITY(1,1) NOT NULL,
	[nome_impressora] [varchar](100) NULL,
 CONSTRAINT [PK_Impressoras] PRIMARY KEY CLUSTERED 
(
	[id_impressora] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tb_itemc]    Script Date: 29/07/2021 10:30:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_itemc](
	[id_item] [int] IDENTITY(1,1) NOT NULL,
	[id_prod] [int] NOT NULL,
	[id_compra] [int] NOT NULL,
	[quant_item] [decimal](18, 3) NOT NULL,
	[subTot_item] [decimal](18, 3) NOT NULL,
	[valor_item] [decimal](18, 3) NOT NULL,
 CONSTRAINT [PK_tb_itemsc] PRIMARY KEY CLUSTERED 
(
	[id_item] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tb_itemcr]    Script Date: 29/07/2021 10:30:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_itemcr](
	[id_item] [int] IDENTITY(1,1) NOT NULL,
	[id_prod] [int] NOT NULL,
	[id_comprar] [int] NOT NULL,
	[quant_item] [decimal](18, 3) NOT NULL,
	[subTot_item] [decimal](18, 3) NOT NULL,
	[valor_item] [decimal](18, 3) NOT NULL,
 CONSTRAINT [PK_tb_itemcr] PRIMARY KEY CLUSTERED 
(
	[id_item] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tb_itemv]    Script Date: 29/07/2021 10:30:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_itemv](
	[id_item] [int] IDENTITY(1,1) NOT NULL,
	[id_prod] [int] NULL,
	[id_venda] [int] NULL,
	[quant_item] [decimal](18, 3) NULL,
	[subTot_item] [decimal](18, 2) NULL,
	[valr_item] [decimal](18, 2) NULL,
 CONSTRAINT [PK_tb_itemv] PRIMARY KEY CLUSTERED 
(
	[id_item] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tb_produtos]    Script Date: 29/07/2021 10:30:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_produtos](
	[id_prod] [int] IDENTITY(1,1) NOT NULL,
	[desc_prod] [varchar](100) NULL,
	[val_prod] [decimal](18, 2) NULL,
	[usuario] [int] NULL,
 CONSTRAINT [PK_tb_produtos] PRIMARY KEY CLUSTERED 
(
	[id_prod] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tb_tipoUsuario]    Script Date: 29/07/2021 10:30:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_tipoUsuario](
	[id_tipoUsuario] [int] IDENTITY(1,1) NOT NULL,
	[desc_tipoUsuario] [varchar](50) NOT NULL,
 CONSTRAINT [PK_tb_tipoUsuario] PRIMARY KEY CLUSTERED 
(
	[id_tipoUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tb_usuario]    Script Date: 29/07/2021 10:30:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_usuario](
	[id_usuario] [int] IDENTITY(1,1) NOT NULL,
	[nome_usuario] [varchar](50) NOT NULL,
	[senha_usuario] [varchar](50) NULL,
	[permi_usuario] [int] NOT NULL,
 CONSTRAINT [PK_tb_usuario] PRIMARY KEY CLUSTERED 
(
	[id_usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tb_venda]    Script Date: 29/07/2021 10:30:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_venda](
	[id_venda] [int] IDENTITY(1,1) NOT NULL,
	[data_venda] [datetime] NOT NULL,
	[valor_nota] [decimal](18, 2) NOT NULL,
	[usuario] [int] NULL,
 CONSTRAINT [PK_tb_vendas] PRIMARY KEY CLUSTERED 
(
	[id_venda] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[tb_caixa]  WITH CHECK ADD  CONSTRAINT [FK_tb_caixa_tb_usuario] FOREIGN KEY([usuario])
REFERENCES [dbo].[tb_usuario] ([id_usuario])
GO
ALTER TABLE [dbo].[tb_caixa] CHECK CONSTRAINT [FK_tb_caixa_tb_usuario]
GO
ALTER TABLE [dbo].[tb_compra]  WITH CHECK ADD  CONSTRAINT [FK_tb_compra_tb_usuario] FOREIGN KEY([usuario])
REFERENCES [dbo].[tb_usuario] ([id_usuario])
GO
ALTER TABLE [dbo].[tb_compra] CHECK CONSTRAINT [FK_tb_compra_tb_usuario]
GO
ALTER TABLE [dbo].[tb_comprar]  WITH CHECK ADD  CONSTRAINT [FK_tb_comprar_tb_usuario] FOREIGN KEY([usuario])
REFERENCES [dbo].[tb_usuario] ([id_usuario])
GO
ALTER TABLE [dbo].[tb_comprar] CHECK CONSTRAINT [FK_tb_comprar_tb_usuario]
GO
ALTER TABLE [dbo].[tb_itemc]  WITH CHECK ADD  CONSTRAINT [FK_tb_itemsc_tb_compra] FOREIGN KEY([id_compra])
REFERENCES [dbo].[tb_compra] ([id_compra])
GO
ALTER TABLE [dbo].[tb_itemc] CHECK CONSTRAINT [FK_tb_itemsc_tb_compra]
GO
ALTER TABLE [dbo].[tb_itemc]  WITH CHECK ADD  CONSTRAINT [FK_tb_itemsc_tb_produtos] FOREIGN KEY([id_prod])
REFERENCES [dbo].[tb_produtos] ([id_prod])
GO
ALTER TABLE [dbo].[tb_itemc] CHECK CONSTRAINT [FK_tb_itemsc_tb_produtos]
GO
ALTER TABLE [dbo].[tb_itemcr]  WITH CHECK ADD  CONSTRAINT [FK_tb_itemcr_tb_comprar] FOREIGN KEY([id_comprar])
REFERENCES [dbo].[tb_comprar] ([id_comprar])
GO
ALTER TABLE [dbo].[tb_itemcr] CHECK CONSTRAINT [FK_tb_itemcr_tb_comprar]
GO
ALTER TABLE [dbo].[tb_itemcr]  WITH CHECK ADD  CONSTRAINT [FK_tb_itemcr_tb_itemcr] FOREIGN KEY([id_prod])
REFERENCES [dbo].[tb_produtos] ([id_prod])
GO
ALTER TABLE [dbo].[tb_itemcr] CHECK CONSTRAINT [FK_tb_itemcr_tb_itemcr]
GO
ALTER TABLE [dbo].[tb_itemv]  WITH CHECK ADD  CONSTRAINT [FK_tb_itemv_tb_produtos] FOREIGN KEY([id_prod])
REFERENCES [dbo].[tb_produtos] ([id_prod])
GO
ALTER TABLE [dbo].[tb_itemv] CHECK CONSTRAINT [FK_tb_itemv_tb_produtos]
GO
ALTER TABLE [dbo].[tb_itemv]  WITH CHECK ADD  CONSTRAINT [FK_tb_itemv_tb_venda] FOREIGN KEY([id_venda])
REFERENCES [dbo].[tb_venda] ([id_venda])
GO
ALTER TABLE [dbo].[tb_itemv] CHECK CONSTRAINT [FK_tb_itemv_tb_venda]
GO
ALTER TABLE [dbo].[tb_usuario]  WITH CHECK ADD  CONSTRAINT [FK_tb_usuario_tb_tipoUsuario] FOREIGN KEY([permi_usuario])
REFERENCES [dbo].[tb_tipoUsuario] ([id_tipoUsuario])
GO
ALTER TABLE [dbo].[tb_usuario] CHECK CONSTRAINT [FK_tb_usuario_tb_tipoUsuario]
GO
ALTER TABLE [dbo].[tb_venda]  WITH CHECK ADD  CONSTRAINT [FK_tb_venda_tb_usuario] FOREIGN KEY([usuario])
REFERENCES [dbo].[tb_usuario] ([id_usuario])
GO
ALTER TABLE [dbo].[tb_venda] CHECK CONSTRAINT [FK_tb_venda_tb_usuario]
GO
