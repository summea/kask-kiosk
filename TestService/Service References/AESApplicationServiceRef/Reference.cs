﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18449
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestService.AESApplicationServiceRef {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="KaskServiceException", Namespace="http://schemas.datacontract.org/2004/07/Kask.Services.Exceptions")]
    [System.SerializableAttribute()]
    public partial class KaskServiceException : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="AESApplicationServiceRef.IApplicationService")]
    public interface IApplicationService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IApplicationService/GetApplicationByID", ReplyAction="http://tempuri.org/IApplicationService/GetApplicationByIDResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(TestService.AESApplicationServiceRef.KaskServiceException), Action="http://tempuri.org/IApplicationService/GetApplicationByIDKaskServiceExceptionFaul" +
            "t", Name="KaskServiceException", Namespace="http://schemas.datacontract.org/2004/07/Kask.Services.Exceptions")]
        Kask.DAL.Models.Application GetApplicationByID(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IApplicationService/GetApplicationByID", ReplyAction="http://tempuri.org/IApplicationService/GetApplicationByIDResponse")]
        System.Threading.Tasks.Task<Kask.DAL.Models.Application> GetApplicationByIDAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IApplicationService/GetApplicationsByName", ReplyAction="http://tempuri.org/IApplicationService/GetApplicationsByNameResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(TestService.AESApplicationServiceRef.KaskServiceException), Action="http://tempuri.org/IApplicationService/GetApplicationsByNameKaskServiceExceptionF" +
            "ault", Name="KaskServiceException", Namespace="http://schemas.datacontract.org/2004/07/Kask.Services.Exceptions")]
        Kask.DAL.Models.Application[] GetApplicationsByName(string first, string last, string ssn);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IApplicationService/GetApplicationsByName", ReplyAction="http://tempuri.org/IApplicationService/GetApplicationsByNameResponse")]
        System.Threading.Tasks.Task<Kask.DAL.Models.Application[]> GetApplicationsByNameAsync(string first, string last, string ssn);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IApplicationService/GetApplications", ReplyAction="http://tempuri.org/IApplicationService/GetApplicationsResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(TestService.AESApplicationServiceRef.KaskServiceException), Action="http://tempuri.org/IApplicationService/GetApplicationsKaskServiceExceptionFault", Name="KaskServiceException", Namespace="http://schemas.datacontract.org/2004/07/Kask.Services.Exceptions")]
        Kask.DAL.Models.Application[] GetApplications();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IApplicationService/GetApplications", ReplyAction="http://tempuri.org/IApplicationService/GetApplicationsResponse")]
        System.Threading.Tasks.Task<Kask.DAL.Models.Application[]> GetApplicationsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IApplicationService/CreateApplication", ReplyAction="http://tempuri.org/IApplicationService/CreateApplicationResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(TestService.AESApplicationServiceRef.KaskServiceException), Action="http://tempuri.org/IApplicationService/CreateApplicationKaskServiceExceptionFault" +
            "", Name="KaskServiceException", Namespace="http://schemas.datacontract.org/2004/07/Kask.Services.Exceptions")]
        bool CreateApplication(Kask.DAL.Models.Application app);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IApplicationService/CreateApplication", ReplyAction="http://tempuri.org/IApplicationService/CreateApplicationResponse")]
        System.Threading.Tasks.Task<bool> CreateApplicationAsync(Kask.DAL.Models.Application app);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IApplicationService/UpdateApplication", ReplyAction="http://tempuri.org/IApplicationService/UpdateApplicationResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(TestService.AESApplicationServiceRef.KaskServiceException), Action="http://tempuri.org/IApplicationService/UpdateApplicationKaskServiceExceptionFault" +
            "", Name="KaskServiceException", Namespace="http://schemas.datacontract.org/2004/07/Kask.Services.Exceptions")]
        bool UpdateApplication(Kask.DAL.Models.Application newApp);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IApplicationService/UpdateApplication", ReplyAction="http://tempuri.org/IApplicationService/UpdateApplicationResponse")]
        System.Threading.Tasks.Task<bool> UpdateApplicationAsync(Kask.DAL.Models.Application newApp);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IApplicationService/DeleteApplication", ReplyAction="http://tempuri.org/IApplicationService/DeleteApplicationResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(TestService.AESApplicationServiceRef.KaskServiceException), Action="http://tempuri.org/IApplicationService/DeleteApplicationKaskServiceExceptionFault" +
            "", Name="KaskServiceException", Namespace="http://schemas.datacontract.org/2004/07/Kask.Services.Exceptions")]
        bool DeleteApplication(int ID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IApplicationService/DeleteApplication", ReplyAction="http://tempuri.org/IApplicationService/DeleteApplicationResponse")]
        System.Threading.Tasks.Task<bool> DeleteApplicationAsync(int ID);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IApplicationServiceChannel : TestService.AESApplicationServiceRef.IApplicationService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ApplicationServiceClient : System.ServiceModel.ClientBase<TestService.AESApplicationServiceRef.IApplicationService>, TestService.AESApplicationServiceRef.IApplicationService {
        
        public ApplicationServiceClient() {
        }
        
        public ApplicationServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ApplicationServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ApplicationServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ApplicationServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public Kask.DAL.Models.Application GetApplicationByID(int id) {
            return base.Channel.GetApplicationByID(id);
        }
        
        public System.Threading.Tasks.Task<Kask.DAL.Models.Application> GetApplicationByIDAsync(int id) {
            return base.Channel.GetApplicationByIDAsync(id);
        }
        
        public Kask.DAL.Models.Application[] GetApplicationsByName(string first, string last, string ssn) {
            return base.Channel.GetApplicationsByName(first, last, ssn);
        }
        
        public System.Threading.Tasks.Task<Kask.DAL.Models.Application[]> GetApplicationsByNameAsync(string first, string last, string ssn) {
            return base.Channel.GetApplicationsByNameAsync(first, last, ssn);
        }
        
        public Kask.DAL.Models.Application[] GetApplications() {
            return base.Channel.GetApplications();
        }
        
        public System.Threading.Tasks.Task<Kask.DAL.Models.Application[]> GetApplicationsAsync() {
            return base.Channel.GetApplicationsAsync();
        }
        
        public bool CreateApplication(Kask.DAL.Models.Application app) {
            return base.Channel.CreateApplication(app);
        }
        
        public System.Threading.Tasks.Task<bool> CreateApplicationAsync(Kask.DAL.Models.Application app) {
            return base.Channel.CreateApplicationAsync(app);
        }
        
        public bool UpdateApplication(Kask.DAL.Models.Application newApp) {
            return base.Channel.UpdateApplication(newApp);
        }
        
        public System.Threading.Tasks.Task<bool> UpdateApplicationAsync(Kask.DAL.Models.Application newApp) {
            return base.Channel.UpdateApplicationAsync(newApp);
        }
        
        public bool DeleteApplication(int ID) {
            return base.Channel.DeleteApplication(ID);
        }
        
        public System.Threading.Tasks.Task<bool> DeleteApplicationAsync(int ID) {
            return base.Channel.DeleteApplicationAsync(ID);
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="AESApplicationServiceRef.IApplicantService")]
    public interface IApplicantService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IApplicantService/GetApplicantByID", ReplyAction="http://tempuri.org/IApplicantService/GetApplicantByIDResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(TestService.AESApplicationServiceRef.KaskServiceException), Action="http://tempuri.org/IApplicantService/GetApplicantByIDKaskServiceExceptionFault", Name="KaskServiceException", Namespace="http://schemas.datacontract.org/2004/07/Kask.Services.Exceptions")]
        Kask.DAL.Models.Applicant GetApplicantByID(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IApplicantService/GetApplicantByID", ReplyAction="http://tempuri.org/IApplicantService/GetApplicantByIDResponse")]
        System.Threading.Tasks.Task<Kask.DAL.Models.Applicant> GetApplicantByIDAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IApplicantService/GetApplicants", ReplyAction="http://tempuri.org/IApplicantService/GetApplicantsResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(TestService.AESApplicationServiceRef.KaskServiceException), Action="http://tempuri.org/IApplicantService/GetApplicantsKaskServiceExceptionFault", Name="KaskServiceException", Namespace="http://schemas.datacontract.org/2004/07/Kask.Services.Exceptions")]
        Kask.DAL.Models.Applicant[] GetApplicants();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IApplicantService/GetApplicants", ReplyAction="http://tempuri.org/IApplicantService/GetApplicantsResponse")]
        System.Threading.Tasks.Task<Kask.DAL.Models.Applicant[]> GetApplicantsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IApplicantService/CreateApplicant", ReplyAction="http://tempuri.org/IApplicantService/CreateApplicantResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(TestService.AESApplicationServiceRef.KaskServiceException), Action="http://tempuri.org/IApplicantService/CreateApplicantKaskServiceExceptionFault", Name="KaskServiceException", Namespace="http://schemas.datacontract.org/2004/07/Kask.Services.Exceptions")]
        bool CreateApplicant(Kask.DAL.Models.Applicant a);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IApplicantService/CreateApplicant", ReplyAction="http://tempuri.org/IApplicantService/CreateApplicantResponse")]
        System.Threading.Tasks.Task<bool> CreateApplicantAsync(Kask.DAL.Models.Applicant a);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IApplicantService/UpdateApplicant", ReplyAction="http://tempuri.org/IApplicantService/UpdateApplicantResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(TestService.AESApplicationServiceRef.KaskServiceException), Action="http://tempuri.org/IApplicantService/UpdateApplicantKaskServiceExceptionFault", Name="KaskServiceException", Namespace="http://schemas.datacontract.org/2004/07/Kask.Services.Exceptions")]
        bool UpdateApplicant(Kask.DAL.Models.Applicant newApp);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IApplicantService/UpdateApplicant", ReplyAction="http://tempuri.org/IApplicantService/UpdateApplicantResponse")]
        System.Threading.Tasks.Task<bool> UpdateApplicantAsync(Kask.DAL.Models.Applicant newApp);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IApplicantService/DeleteApplicant", ReplyAction="http://tempuri.org/IApplicantService/DeleteApplicantResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(TestService.AESApplicationServiceRef.KaskServiceException), Action="http://tempuri.org/IApplicantService/DeleteApplicantKaskServiceExceptionFault", Name="KaskServiceException", Namespace="http://schemas.datacontract.org/2004/07/Kask.Services.Exceptions")]
        bool DeleteApplicant(int ID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IApplicantService/DeleteApplicant", ReplyAction="http://tempuri.org/IApplicantService/DeleteApplicantResponse")]
        System.Threading.Tasks.Task<bool> DeleteApplicantAsync(int ID);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IApplicantServiceChannel : TestService.AESApplicationServiceRef.IApplicantService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ApplicantServiceClient : System.ServiceModel.ClientBase<TestService.AESApplicationServiceRef.IApplicantService>, TestService.AESApplicationServiceRef.IApplicantService {
        
        public ApplicantServiceClient() {
        }
        
        public ApplicantServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ApplicantServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ApplicantServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ApplicantServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public Kask.DAL.Models.Applicant GetApplicantByID(int id) {
            return base.Channel.GetApplicantByID(id);
        }
        
        public System.Threading.Tasks.Task<Kask.DAL.Models.Applicant> GetApplicantByIDAsync(int id) {
            return base.Channel.GetApplicantByIDAsync(id);
        }
        
        public Kask.DAL.Models.Applicant[] GetApplicants() {
            return base.Channel.GetApplicants();
        }
        
        public System.Threading.Tasks.Task<Kask.DAL.Models.Applicant[]> GetApplicantsAsync() {
            return base.Channel.GetApplicantsAsync();
        }
        
        public bool CreateApplicant(Kask.DAL.Models.Applicant a) {
            return base.Channel.CreateApplicant(a);
        }
        
        public System.Threading.Tasks.Task<bool> CreateApplicantAsync(Kask.DAL.Models.Applicant a) {
            return base.Channel.CreateApplicantAsync(a);
        }
        
        public bool UpdateApplicant(Kask.DAL.Models.Applicant newApp) {
            return base.Channel.UpdateApplicant(newApp);
        }
        
        public System.Threading.Tasks.Task<bool> UpdateApplicantAsync(Kask.DAL.Models.Applicant newApp) {
            return base.Channel.UpdateApplicantAsync(newApp);
        }
        
        public bool DeleteApplicant(int ID) {
            return base.Channel.DeleteApplicant(ID);
        }
        
        public System.Threading.Tasks.Task<bool> DeleteApplicantAsync(int ID) {
            return base.Channel.DeleteApplicantAsync(ID);
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="AESApplicationServiceRef.IAppliedService")]
    public interface IAppliedService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAppliedService/GetAppliedByID", ReplyAction="http://tempuri.org/IAppliedService/GetAppliedByIDResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(TestService.AESApplicationServiceRef.KaskServiceException), Action="http://tempuri.org/IAppliedService/GetAppliedByIDKaskServiceExceptionFault", Name="KaskServiceException", Namespace="http://schemas.datacontract.org/2004/07/Kask.Services.Exceptions")]
        Kask.DAL.Models.Applied GetAppliedByID(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAppliedService/GetAppliedByID", ReplyAction="http://tempuri.org/IAppliedService/GetAppliedByIDResponse")]
        System.Threading.Tasks.Task<Kask.DAL.Models.Applied> GetAppliedByIDAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAppliedService/GetApplieds", ReplyAction="http://tempuri.org/IAppliedService/GetAppliedsResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(TestService.AESApplicationServiceRef.KaskServiceException), Action="http://tempuri.org/IAppliedService/GetAppliedsKaskServiceExceptionFault", Name="KaskServiceException", Namespace="http://schemas.datacontract.org/2004/07/Kask.Services.Exceptions")]
        Kask.DAL.Models.Applied[] GetApplieds();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAppliedService/GetApplieds", ReplyAction="http://tempuri.org/IAppliedService/GetAppliedsResponse")]
        System.Threading.Tasks.Task<Kask.DAL.Models.Applied[]> GetAppliedsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAppliedService/CreateApplied", ReplyAction="http://tempuri.org/IAppliedService/CreateAppliedResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(TestService.AESApplicationServiceRef.KaskServiceException), Action="http://tempuri.org/IAppliedService/CreateAppliedKaskServiceExceptionFault", Name="KaskServiceException", Namespace="http://schemas.datacontract.org/2004/07/Kask.Services.Exceptions")]
        bool CreateApplied(Kask.DAL.Models.Applied a);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAppliedService/CreateApplied", ReplyAction="http://tempuri.org/IAppliedService/CreateAppliedResponse")]
        System.Threading.Tasks.Task<bool> CreateAppliedAsync(Kask.DAL.Models.Applied a);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAppliedService/DeleteApplied", ReplyAction="http://tempuri.org/IAppliedService/DeleteAppliedResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(TestService.AESApplicationServiceRef.KaskServiceException), Action="http://tempuri.org/IAppliedService/DeleteAppliedKaskServiceExceptionFault", Name="KaskServiceException", Namespace="http://schemas.datacontract.org/2004/07/Kask.Services.Exceptions")]
        bool DeleteApplied(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAppliedService/DeleteApplied", ReplyAction="http://tempuri.org/IAppliedService/DeleteAppliedResponse")]
        System.Threading.Tasks.Task<bool> DeleteAppliedAsync(int id);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAppliedServiceChannel : TestService.AESApplicationServiceRef.IAppliedService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AppliedServiceClient : System.ServiceModel.ClientBase<TestService.AESApplicationServiceRef.IAppliedService>, TestService.AESApplicationServiceRef.IAppliedService {
        
        public AppliedServiceClient() {
        }
        
        public AppliedServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public AppliedServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AppliedServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AppliedServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public Kask.DAL.Models.Applied GetAppliedByID(int id) {
            return base.Channel.GetAppliedByID(id);
        }
        
        public System.Threading.Tasks.Task<Kask.DAL.Models.Applied> GetAppliedByIDAsync(int id) {
            return base.Channel.GetAppliedByIDAsync(id);
        }
        
        public Kask.DAL.Models.Applied[] GetApplieds() {
            return base.Channel.GetApplieds();
        }
        
        public System.Threading.Tasks.Task<Kask.DAL.Models.Applied[]> GetAppliedsAsync() {
            return base.Channel.GetAppliedsAsync();
        }
        
        public bool CreateApplied(Kask.DAL.Models.Applied a) {
            return base.Channel.CreateApplied(a);
        }
        
        public System.Threading.Tasks.Task<bool> CreateAppliedAsync(Kask.DAL.Models.Applied a) {
            return base.Channel.CreateAppliedAsync(a);
        }
        
        public bool DeleteApplied(int id) {
            return base.Channel.DeleteApplied(id);
        }
        
        public System.Threading.Tasks.Task<bool> DeleteAppliedAsync(int id) {
            return base.Channel.DeleteAppliedAsync(id);
        }
    }
}