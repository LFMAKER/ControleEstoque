class FornecedorModel {
  int id;
  String nome;
  String razaoSocial;
  String numDocumento;
  int tipo;
  String telefone;
  String contato;
  String logradouro;
  String numero;
  String complemento;
  String cep;
  String bairro;
  String cidade;
  String estado;
  String pais;
  bool ativo;

  FornecedorModel(
      {this.id,
      this.nome,
      this.razaoSocial,
      this.numDocumento,
      this.tipo,
      this.telefone,
      this.contato,
      this.logradouro,
      this.numero,
      this.complemento,
      this.cep,
      this.bairro,
      this.cidade,
      this.estado,
      this.pais,
      this.ativo});

  FornecedorModel.fromJson(Map<String, dynamic> json) {
    id = json['Id'];
    nome = json['Nome'];
    razaoSocial = json['RazaoSocial'];
    numDocumento = json['NumDocumento'];
    tipo = json['Tipo'];
    telefone = json['Telefone'];
    contato = json['Contato'];
    logradouro = json['Logradouro'];
    numero = json['Numero'];
    complemento = json['Complemento'];
    cep = json['Cep'];
    bairro = json['Bairro'];
    cidade = json['Cidade'];
    estado = json['Estado'];
    pais = json['Pais'];
    ativo = json['Ativo'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['Id'] = this.id;
    data['Nome'] = this.nome;
    data['RazaoSocial'] = this.razaoSocial;
    data['NumDocumento'] = this.numDocumento;
    data['Tipo'] = this.tipo;
    data['Telefone'] = this.telefone;
    data['Contato'] = this.contato;
    data['Logradouro'] = this.logradouro;
    data['Numero'] = this.numero;
    data['Complemento'] = this.complemento;
    data['Cep'] = this.cep;
    data['Bairro'] = this.bairro;
    data['Cidade'] = this.cidade;
    data['Estado'] = this.estado;
    data['Pais'] = this.pais;
    data['Ativo'] = this.ativo;
    return data;
  }
}