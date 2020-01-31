Select top 1500 c.finalizacaseobs,c.finalizar_Case_Especifico, c.valorDespesa,	c.id,	c.cartao,	c.dataRegistro,	c.estabelecimentoCodigo, f.Finalizacaocase,	sf.Subfinalizacaocase,c.FinalizarCase,c.erroFinalizaCase,c.DataFinalizacaoCase, c.Tratado_Automacao_CASE FROM MX_bMaTRiX c left join MX_sysFinalizacao f on f.id = c.Finalizacao_ID left join MX_sysSubFinalizacao sf on sf.id = c.Subfinalizacao_ID where finalizacaseobs like '%tratado.%' and C.dataCat between Format(DATEADD(day,-20,GETDATE()),'yyyy-MM-dd') and Format(DATEADD(day,-5,GETDATE()),'yyyy-MM-dd') order by c.horafinal asc

select * from mx_bmatrix where id = 703575

select * from mx_bmatrix where cartao = '375177001488983' order by horafinal asc

Select c.finalizacaseobs, c.finalizar_Case_Especifico, c.valorDespesa,	c.id,	c.cartao,
	c.dataRegistro,	c.estabelecimentoCodigo, f.Finalizacaocase,	sf.Subfinalizacaocase,c.FinalizarCase,
	c.erroFinalizaCase,c.DataFinalizacaoCase, c.Tratado_Automacao_CASE  FROM MX_bMaTRiX c left join 
	MX_sysFinalizacao f on f.id = c.Finalizacao_ID left join MX_sysSubFinalizacao sf on sf.id = c.Subfinalizacao_ID 
	where c.FinalizarCase = 1 and c.erroFinalizaCase = 0 and not f.finalizacaoCase is null and c.origemregistro = 'case' 
 order by c.horafinal asc

 	 and C.dataCat >= Format(DATEADD(day,-5,GETDATE()),'yyyy-MM-dd') 


Select c.finalizaCaseOBS,c.finalizar_Case_Especifico, c.valorDespesa,	c.id,	c.cartao,	c.dataRegistro,	c.estabelecimentoCodigo, f.Finalizacaocase,	sf.Subfinalizacaocase,c.FinalizarCase,c.erroFinalizaCase,c.DataFinalizacaoCase, c.Tratado_Automacao_CASE  FROM MX_bMaTRiX c left join MX_sysFinalizacao f on f.id = c.Finalizacao_ID left join MX_sysSubFinalizacao sf on sf.id = c.Subfinalizacao_ID 

where c.FinalizarCase = 1 and c.erroFinalizaCase = 0  and not f.finalizacaoCase is null and c.origemregistro = 'case'   order by c.horafinal asc

select * from MX_bMaTRiX where finalizaCaseOBS like '%Histórico%' order by DataFinalizacaoCase asc

select * from MX_bMaTRiX where id=27669


Select c.finalizaCaseOBS ,c.finalizar_Case_Especifico, c.valorDespesa,	c.id,	c.cartao,	c.dataRegistro,	c.estabelecimentoCodigo, f.Finalizacaocase,	sf.Subfinalizacaocase,c.FinalizarCase,c.erroFinalizaCase,c.DataFinalizacaoCase, c.Tratado_Automacao_CASE FROM MX_bMaTRiX c left join MX_sysFinalizacao f on f.id = c.Finalizacao_ID left join MX_sysSubFinalizacao sf on sf.id = c.Subfinalizacao_ID where c.erroFinalizaCase = 1 and finalizarcase = 1 and not f.finalizacaoCase is null and c.origemregistro = 'case' and c.cartao in ('377169795652005','377169037236005','375134885911002')  order by c.horafinal asc


select * from MX_bMaTRiX where DataFinalizacaoCase between '2018-06-04 11:00:00' and '2018-06-04 23:59:59'
Select c.finalizaCaseOBS ,c.finalizar_Case_Especifico, c.valorDespesa	c.id,	c.cartao,	c.dataRegistro,	c.estabelecimentoCodigo, f.Finalizacaocase,	sf.Subfinalizacaocase,c.FinalizarCase,c.erroFinalizaCase,c.DataFinalizacaoCase, c.Tratado_Automacao_CASE FROM MX_bMaTRiX c left join MX_sysFinalizacao f on f.id = c.Finalizacao_ID left join MX_sysSubFinalizacao sf on sf.id = c.Subfinalizacao_ID where c.FinalizarCase = 1 and c.erroFinalizaCase = 0 and not f.finalizacaoCase is null and c.origemregistro = 'case'   order by c.horafinal asc

use db_fraude_amex


Select c.finalizaCaseOBS,  c.finalizar_Case_Especifico, c.valorDespesa,	c.id,	c.cartao,	c.dataRegistro,	c.estabelecimentoCodigo, f.Finalizacaocase,	sf.Subfinalizacaocase,c.FinalizarCase,c.erroFinalizaCase,c.DataFinalizacaoCase, c.Tratado_Automacao_CASE  FROM MX_bMaTRiX c left join MX_sysFinalizacao f on f.id = c.Finalizacao_ID left join MX_sysSubFinalizacao sf on sf.id = c.Subfinalizacao_ID where c.FinalizarCase = 1 and c.erroFinalizaCase = 0 and not f.finalizacaoCase is null and c.origemregistro = 'case'  and C.dataCat between '2018-05-01' and '2018-05-31'  order by c.horafinal asc