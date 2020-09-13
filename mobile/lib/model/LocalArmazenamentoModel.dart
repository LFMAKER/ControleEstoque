class LocalArmazenamentoModel {
  int id;
  String nome;
  int capacidadeTotal;
  int capacidadeAtual;
  bool ativo;

  LocalArmazenamentoModel(
      {this.id,
      this.nome,
      this.capacidadeTotal,
      this.capacidadeAtual,
      this.ativo});

  LocalArmazenamentoModel.fromJson(Map<String, dynamic> json) {
    id = json['Id'];
    nome = json['Nome'];
    capacidadeTotal = json['CapacidadeTotal'];
    capacidadeAtual = json['CapacidadeAtual'];
    ativo = json['Ativo'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['Id'] = this.id;
    data['Nome'] = this.nome;
    data['CapacidadeTotal'] = this.capacidadeTotal;
    data['CapacidadeAtual'] = this.capacidadeAtual;
    data['Ativo'] = this.ativo;
    return data;
  }
}