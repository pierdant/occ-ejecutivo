
var PaintExperienceData = React.createClass({
  render: function() {
    return (
      <div>
        <p><span className="occ_label">Nombre de la compañ&iacute;a</span>&nbsp;<input className="occ_edit_box" id="experience_company_name" title="Nombre de la compañ&iacute;a" defaultValue={this.data.company_name}/></p>
        <p><span className="occ_label">Puesto desempeñado</span>&nbsp;<input className="occ_edit_box" id="experience_position_title" title="Titulo o Puesto desempeñado" defaultValue={this.data.position}/></p>
        <p><span className="occ_label">Periodo</span>&nbsp;<input className="occ_edit_box" id="experience_from" title="Desde" defaultValue={this.data.initial_period} />&nbsp;a&nbsp;<input className="occ_edit_box" id="experience_to" title="Hasta" defaultValue={this.data.final_period}/></p>
        <p><span className="occ_label">Ubicaci&oacute;n</span>&nbsp;<input className="occ_edit_box" id="experience_location" title="Localidad / Ubicaci&oacute;n" defaultValue={this.data.location}/></p>
        <p><span className="occ_label">¿Es su trabajo actual?</span>&nbsp;<input className="occ_edit_box" id="experience_actual" title="Si o no es su experiencia actual" defaultValue={this.data.current_job}/></p>
        <p><span className="occ_label">Descripci&oacute;n</span>&nbsp;<input className="occ_edit_box" id="experience_description" title="Funciones desempeñadas" defaultValue={this.data.description}/></p>
        <br />
        <br />
        <br />
      </div>
    );
  }
});

var GetExperienceData = React.createClass({
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
      return <div>no data yet</div>
    }

/// asi deberia de ser, pero el experieceid debería ser UNICO! (pinche Abel!):
//return (
//  <div>
//  {this.state.userData.experiences.items.map(function(result) {
//    return <PaintExperienceData key={result.experienceid} data={result}/>;
//  })}
//  </div>
//);


    return (
      <div>
      {this.state.userData.experiences.items.map((exp) => (
        <div>
          <p><span className="occ_label">Nombre de la compañ&iacute;a</span>&nbsp;<input className="occ_edit_box" id="experience_company_name" title="Nombre de la compañ&iacute;a" defaultValue={exp.company_name}/></p>
          <p><span className="occ_label">Puesto desempeñado</span>&nbsp;<input className="occ_edit_box" id="experience_position_title" title="Titulo o Puesto desempeñado" defaultValue={exp.position}/></p>
          <p><span className="occ_label">Periodo</span>&nbsp;<input className="occ_edit_box" id="experience_from" title="Desde" defaultValue={exp.initial_period} />&nbsp;a&nbsp;<input className="occ_edit_box" id="experience_to" title="Hasta" defaultValue={exp.final_period}/></p>
          <p><span className="occ_label">Ubicaci&oacute;n</span>&nbsp;<input className="occ_edit_box" id="experience_location" title="Localidad / Ubicaci&oacute;n" defaultValue={exp.location}/></p>
          <p><span className="occ_label">¿Es su trabajo actual?</span>&nbsp;<input className="occ_edit_box" id="experience_actual" title="Si o no es su experiencia actual" defaultValue={exp.current_job}/></p>
          <p><span className="occ_label">Descripci&oacute;n</span>&nbsp;<input className="occ_edit_box" id="experience_description" title="Funciones desempeñadas" defaultValue={exp.description}/></p>
          <br />
          <br />
          <br />
        </div>
      ))}
      </div>
    );
  }
});



var UserExperienceData = React.createClass({
  render: function() {
    return (
      <GetExperienceData urlGetProfData="http://10.10.30.92:5000/applicants/v3/resumes/123123123/experiences/?access_token=123123123123123" pollInterval={5000} />
    );
  }
});


ReactDOM.render(
  <UserExperienceData />, document.getElementById('experience')
);
