import React, { Component } from "react";
import { DataTable } from "primereact/datatable";
import { variables } from "./Variables.js";
import { Button } from "primereact/button";
import AddDepModal from "./AddDepModal";
import EditDepModal from "./EditDepModal";

export class Department extends Component {
  constructor(props) {
    super(props);
    this.state = { deps: [], addModalShow: false, editModalShow: false };
  }

  refreshList() {
    //fetch('https://kglsslvpn.kwglobal.com:10443/proxy/3b149312/http/10.22.11.101:88/api/Department')
    fetch("https://localhost:7128/api/Department")
      .then((response) => response.json())
      .then((responseData) => {
        if (Array.isArray(responseData.result)) {
          const data = responseData.result;
          // Accumulate data in a separate array
          const deps = data.map((item) => {
            return {
              // Map the properties you want to keep
              id: item.id,
              name: item.name,
              isActive: item.isActive,
              // Add more properties as needed
            };
          });
          // Set the state once with the accumulated data
          this.setState({ deps });
        } else {
          console.error("API response result is not an array:", responseData);
        }
      })
      .catch((error) => {
        console.error("Fetch error:", error);
      });
  }

  componentDidMount() {
    this.refreshList();
  }

  componentDidUpdate() {
    this.refreshList();
  }

  render() {
    const { deps, depid, depname } = this.state;
    let addModalClose = () => this.setState({ addModalShow: false });
    let editModalClose = () => this.setState({ editModalShow: false });
    return (
      <div>
        <DataTable className="mt-4" striped bordered hover size="sm">
          <thead>
            <tr>
              <th>DepartmentId</th>
              <th>DepartmentName</th>
            </tr>
          </thead>
          <tbody>
            {deps.map((dep) => (
              <tr>
                <td>{dep.id}</td>
                <td>{dep.name}</td>
                <td>
                  <Button
                    className="mr-2"
                    variant="info"
                    onClick={() =>
                      this.setState({
                        editModalShow: true,
                        depid: dep.DepartmentId,
                        depname: dep.DepartmentName,
                      })
                    }
                  >
                    Edit
                  </Button>

                  <Button
                    className="mr-2"
                    variant="danger"
                    onClick={() => this.deleteDep(dep.DepartmentId)}
                  >
                    Delete
                  </Button>

                  <EditDepModal
                    show={this.state.editModalShow}
                    onHide={editModalClose}
                    depid={depid}
                    depname={depname}
                  />
                </td>
              </tr>
            ))}
          </tbody>
        </DataTable>

        <Button
          variant="primary"
          onClick={() => this.setState({ addModalShow: true })}
        >
          Add Department
        </Button>

        <AddDepModal show={this.state.addModalShow} onHide={addModalClose} />
      </div>
    );
  }
}
