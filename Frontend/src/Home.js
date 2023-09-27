import React,{Component} from 'react';
import {Table} from 'react-bootstrap';
import { variables } from './Variables.js';    
import {Button,ButtonToolbar} from 'react-bootstrap';


export class Home extends Component{

    constructor(props){
        super(props);
        this.state={MRFSummary:[],ResumeSummary:[],InterviewSummary:[]}
    }

    
    refreshMRFSummaryList(){
        
        
            //fetch('https://kglsslvpn.kwglobal.com:10443/proxy/3b149312/http/10.22.11.101:88/api/Department')
            fetch('https://localhost:7128/api/Dashboard/GetMrfStatusSummary')
            .then(response => response.json())
            .then(responseData => {
              if (Array.isArray(responseData.result)) {
                const data = responseData.result;
                // Accumulate data in a separate array
                const MRFSummary = data.map(item => {
                  return {
                    // Map the properties you want to keep
                    id: item.id,
                    referenceNo: item.referenceNo,
                    count: item.statusCount,
                    // Add more properties as needed
                  };
                });
                // Set the state once with the accumulated data
                this.setState({ MRFSummary });
              } else {
                console.error('API response result is not an array:', responseData);
              }
            })
            .catch(error => {
              console.error('Fetch error:', error);
            });
          
        }

        refreshResumeSummary(){
        
        
            //fetch('https://kglsslvpn.kwglobal.com:10443/proxy/3b149312/http/10.22.11.101:88/api/Department')
            fetch('https://localhost:7128/api/Dashboard/GetMrfResumeSummary')
            .then(response => response.json())
            .then(responseData => {
              if (Array.isArray(responseData.result)) {
                const data = responseData.result;
                // Accumulate data in a separate array
                const ResumeSummary = data.map(item => {
                  return {
                    // Map the properties you want to keep
                   
                    id: item.id,
                    status: item.candidatestatus,
                    count: item.statusCount,
                    // Add more properties as needed
                  };
                });
                // Set the state once with the accumulated data
                this.setState({ ResumeSummary });
              } else {
                console.error('API response result is not an array:', responseData);
              }
            })
            .catch(error => {
              console.error('Fetch error:', error);
            });
          
        }

        refreshInterviewSummary(){
        
        
            //fetch('https://kglsslvpn.kwglobal.com:10443/proxy/3b149312/http/10.22.11.101:88/api/Department')
            fetch('https://localhost:7128/api/Dashboard/GetMrfInterviewSummary')
            .then(response => response.json())
            .then(responseData => {
              if (Array.isArray(responseData.result)) {
                const data = responseData.result;
                // Accumulate data in a separate array
                const InterviewSummary = data.map(item => {
                  return {
                    // Map the properties you want to keep
                    id: item.id,
                    status: item.candidatestatus,
                    count: item.statusCount,
                    // Add more properties as needed
                  };
                });
                // Set the state once with the accumulated data
                this.setState({ InterviewSummary });
              } else {
                console.error('API response result is not an array:', responseData);
              }
            })
            .catch(error => {
              console.error('Fetch error:', error);
            });
          
        }
  
    componentDidMount(){
        this.refreshMRFSummaryList();
        this.refreshResumeSummary();
        this.refreshInterviewSummary();
    }

    componentDidUpdate(){
        this.refreshMRFSummaryList();
        this.refreshResumeSummary();
        this.refreshInterviewSummary();
    }

   
    render(){
        const {MRFSummary,ResumeSummary,InterviewSummary}=this.state;
        
        return(
            <div>
                <Table className="mt-4" striped bordered hover size="sm">
                    <thead>
                        <tr>
                            <th>MRF ReferenceNo</th>
                            <th>Count</th>
                       
                        </tr>
                    </thead>
                    <tbody>
                        {   MRFSummary.map(MRFSummary=>
                            <tr>
                                <td>{MRFSummary.referenceNo}</td>
                                <td>{MRFSummary.count}</td>
                            </tr>
                        )}
                    </tbody>
                     </Table>

                     <Table className="mt-4" striped bordered hover size="sm">
                    <thead>
                        <tr>
                            <th>MRF Resume Status</th>
                            <th>Count</th>
                       
                        </tr>
                    </thead>
                    <tbody>
                        {   ResumeSummary.map(ResumeSummary=>
                            <tr>
                                 <td>{ResumeSummary.status}</td>
                                <td>{ResumeSummary.count}</td>
                            </tr>
                        )}
                    </tbody>
                     </Table>
                     <Table className="mt-4" striped bordered hover size="sm">
                    <thead>
                        <tr>
                            <th>MRF Interview Status</th>
                            <th>Count</th>
                       
                        </tr>
                    </thead>
                    <tbody>
                        {   InterviewSummary.map(InterviewSummary=>
                            <tr>
                                <td>{InterviewSummary.status}</td>
                                <td>{InterviewSummary.count}</td>
                            </tr>
                        )}
                    </tbody>
                     </Table>
            </div>

            
          
        )
    }
}