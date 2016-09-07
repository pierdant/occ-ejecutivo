
var PaintUserData = React.createClass({
  render: function() {
    return (
      <div>
      <p><span className="occ_label">Nombre</span>&nbsp;<input className="occ_edit_box" id="cand_name" title="Nombre" defaultValue={this.props.name} /></p>
      <p><span className="occ_label">Apellido Paterno</span>&nbsp;<input className="occ_edit_box" id="cand_lastname_1" title="Apellido Paterno" defaultValue={this.props.lastname1} /></p>
      <p><span className="occ_label">Apellido Materno</span>&nbsp;<input className="occ_edit_box" id="cand_lastname_2" title="Apellido Materno" defaultValue={this.props.lastname2} /></p>
      <p><span className="occ_label">Correo</span>&nbsp;<input className="occ_edit_box" id="cand_email" title="Email" defaultValue={this.props.email} /></p>
      <p><span className="occ_label">T&iacute;tulo</span>&nbsp;<input className="occ_edit_box" id="curr_title" title="Titulo" defaultValue={this.props.title} /></p>
      </div>
    );
  }
});

var GetContactData = React.createClass({
  loadCommentsFromServer: function() {
    $.ajax({
      url: this.props.urlGetProfData,
      dataType: 'json',
      cache: false,
      success: function(data) {
        this.setState({userData:data});
      }.bind(this),
      error: function(xhr, status, err) {
        console.error(this.props.urlGetProfData, status, err.toString());
      }.bind(this)
    });
  },
  getInitialState: function() {
    return { userData: undefined };
  },
  componentDidMount: function() {
    this.loadCommentsFromServer();
    //setInterval(this.loadCommentsFromServer, this.props.pollInterval);
  },
  render: function() {

    if( !this.state.userData ){
      return <div>no response yet</div>
    }

    if( !this.state.userData.length === 0 ){
      return <div>no-name</div>
    }

    var userFields = this.state.userData.account.map(function(exp) {
      return (
        <PaintUserData key={exp.id} name={exp.name} lastname1={exp.lastname} lastname2={exp.lastname} email={exp.email} title={exp.office_phone} />
      );
    });
    return (
      <div>
        {userFields}
      </div>
    );
  }
});



var UserContactData = React.createClass({
  render: function() {
    return (
      <GetContactData urlGetProfData="http://10.10.30.92:5001/users/v3/accounts/123123123123?access_token=123123123123123" pollInterval={5000} />
    );
  }
});


ReactDOM.render(
  <UserContactData />,
  document.getElementById('datos_usuario')
);
