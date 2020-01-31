select * from MX_bMaTRiX where FinalizarCase = 1


Select c.valorDespesa,	c.id,	c.cartao,	c.dataRegistro,	c.estabelecimentoCodigo, f.Finalizacaocase,	sf.Subfinalizacaocase,c.FinalizarCase,c.erroFinalizaCase,c.DataFinalizacaoCase FROM MX_bMaTRiX c left join MX_sysFinalizacao f on f.id = c.Finalizacao_ID left join MX_sysSubFinalizacao sf on sf.id = c.Subfinalizacao_ID where c.FinalizarCase = 1 and c.erroFinalizaCase = 0

select * from MX_sysFinalizacao where descricao = 'descarte'

UPDATE MX_bMaTRiX SET Finalizarcase = 0, DataFinalizacaoCase = getdate() , erroFinalizaCase = 0 where id = 263706
UPDATE MX_bMaTRiX SET Finalizarcase = " & obj.iFinalizarCase & ",DataFinalizacaoCase = '" & cone.dataSql(obj.sDataFinalizacaoCase) & "' , erroFinalizaCase = " & obj.ierroFinalizaCase & " WHERE id = " & obj.iCodMatrix & "

select * from MX_bMaTRiX where id = 263706

