class Usuario {
  int id;
  String login;
  String senha;
  String nome;
  String email;
  int idPerfil;
  Perfil perfil;
  KeyC key;

  Usuario(
      {this.id,
        this.login,
        this.senha,
        this.nome,
        this.email,
        this.idPerfil,
        this.perfil,
        this.key});

  Usuario.fromJson(Map<String, dynamic> json) {
    id = json['Id'];
    login = json['Login'];
    senha = json['Senha'];
    nome = json['Nome'];
    email = json['Email'];
    idPerfil = json['IdPerfil'];
    perfil =
    json['Perfil'] != null ? new Perfil.fromJson(json['Perfil']) : null;
    key = json['KeyC'] != null ? new KeyC.fromJson(json['KeyC']) : null;
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['Id'] = this.id;
    data['Login'] = this.login;
    data['Senha'] = this.senha;
    data['Nome'] = this.nome;
    data['Email'] = this.email;
    data['IdPerfil'] = this.idPerfil;
    if (this.perfil != null) {
      data['Perfil'] = this.perfil.toJson();
    }
    if (this.key != null) {
      data['KeyC'] = this.key.toJson();
    }
    return data;
  }


  @override
  String toString() {
    return "ID" + this.id.toString() + " - NOME: " + this.nome + " - EMAIL: " + this.email;
  }
}

class Perfil {
  int id;
  String nome;
  bool ativo;

  Perfil({this.id, this.nome, this.ativo});

  Perfil.fromJson(Map<String, dynamic> json) {
    id = json['Id'];
    nome = json['Nome'];
    ativo = json['Ativo'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['Id'] = this.id;
    data['Nome'] = this.nome;
    data['Ativo'] = this.ativo;
    return data;
  }
}

class KeyC {
  int id;
  String codigo;
  String criadaEm;
  String ultimoUso;
  int quantidadeDeChamadas;
  bool ativada;

  KeyC(
      {this.id,
        this.codigo,
        this.criadaEm,
        this.ultimoUso,
        this.quantidadeDeChamadas,
        this.ativada});

  KeyC.fromJson(Map<String, dynamic> json) {
    id = json['Id'];
    codigo = json['Codigo'];
    criadaEm = json['CriadaEm'];
    ultimoUso = json['UltimoUso'];
    quantidadeDeChamadas = json['QuantidadeDeChamadas'];
    ativada = json['Ativada'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['Id'] = this.id;
    data['Codigo'] = this.codigo;
    data['CriadaEm'] = this.criadaEm;
    data['UltimoUso'] = this.ultimoUso;
    data['QuantidadeDeChamadas'] = this.quantidadeDeChamadas;
    data['Ativada'] = this.ativada;
    return data;
  }

}