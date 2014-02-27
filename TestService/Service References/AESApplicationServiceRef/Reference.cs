﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34011
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestService.AESApplicationServiceRef {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="AESApplicationServiceRef.IApplicationService")]
    public interface IApplicationService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IApplicationService/GetApplicationById", ReplyAction="http://tempuri.org/IApplicationService/GetApplicationByIdResponse")]
        Kask.DAL2.Models.Application GetApplicationById(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IApplicationService/GetApplicationById", ReplyAction="http://tempuri.org/IApplicationService/GetApplicationByIdResponse")]
        System.Threading.Tasks.Task<Kask.DAL2.Models.Application> GetApplicationByIdAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IApplicationService/GetApplications", ReplyAction="http://tempuri.org/IApplicationService/GetApplicationsResponse")]
        Kask.DAL2.Models.Application[] GetApplications();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IApplicationService/GetApplications", ReplyAction="http://tempuri.org/IApplicationService/GetApplicationsResponse")]
        System.Threading.Tasks.Task<Kask.DAL2.Models.Application[]> GetApplicationsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IApplicationService/CreateApplication", ReplyAction="http://tempuri.org/IApplicationService/CreateApplicationResponse")]
        bool CreateApplication(Kask.DAL2.Models.Application app);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IApplicationService/CreateApplication", ReplyAction="http://tempuri.org/IApplicationService/CreateApplicationResponse")]
        System.Threading.Tasks.Task<bool> CreateApplicationAsync(Kask.DAL2.Models.Application app);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IApplicationService/UpdateApplication", ReplyAction="http://tempuri.org/IApplicationService/UpdateApplicationResponse")]
        bool UpdateApplication(Kask.DAL2.Models.Application newApp);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IApplicationService/UpdateApplication", ReplyAction="http://tempuri.org/IApplicationService/UpdateApplicationResponse")]
        System.Threading.Tasks.Task<bool> UpdateApplicationAsync(Kask.DAL2.Models.Application newApp);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IApplicationService/DeleteApplication", ReplyAction="http://tempuri.org/IApplicationService/DeleteApplicationResponse")]
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
        
        public Kask.DAL2.Models.Application GetApplicationById(int id) {
            return base.Channel.GetApplicationById(id);
        }
        
        public System.Threading.Tasks.Task<Kask.DAL2.Models.Application> GetApplicationByIdAsync(int id) {
            return base.Channel.GetApplicationByIdAsync(id);
        }
        
        public Kask.DAL2.Models.Application[] GetApplications() {
            return base.Channel.GetApplications();
        }
        
        public System.Threading.Tasks.Task<Kask.DAL2.Models.Application[]> GetApplicationsAsync() {
            return base.Channel.GetApplicationsAsync();
        }
        
        public bool CreateApplication(Kask.DAL2.Models.Application app) {
            return base.Channel.CreateApplication(app);
        }
        
        public System.Threading.Tasks.Task<bool> CreateApplicationAsync(Kask.DAL2.Models.Application app) {
            return base.Channel.CreateApplicationAsync(app);
        }
        
        public bool UpdateApplication(Kask.DAL2.Models.Application newApp) {
            return base.Channel.UpdateApplication(newApp);
        }
        
        public System.Threading.Tasks.Task<bool> UpdateApplicationAsync(Kask.DAL2.Models.Application newApp) {
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
        Kask.DAL2.Models.Applicant GetApplicantByID(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IApplicantService/GetApplicantByID", ReplyAction="http://tempuri.org/IApplicantService/GetApplicantByIDResponse")]
        System.Threading.Tasks.Task<Kask.DAL2.Models.Applicant> GetApplicantByIDAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IApplicantService/GetApplicants", ReplyAction="http://tempuri.org/IApplicantService/GetApplicantsResponse")]
        Kask.DAL2.Models.Applicant[] GetApplicants();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IApplicantService/GetApplicants", ReplyAction="http://tempuri.org/IApplicantService/GetApplicantsResponse")]
        System.Threading.Tasks.Task<Kask.DAL2.Models.Applicant[]> GetApplicantsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IApplicantService/CreateApplicant", ReplyAction="http://tempuri.org/IApplicantService/CreateApplicantResponse")]
        bool CreateApplicant(Kask.DAL2.Models.Applicant a);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IApplicantService/CreateApplicant", ReplyAction="http://tempuri.org/IApplicantService/CreateApplicantResponse")]
        System.Threading.Tasks.Task<bool> CreateApplicantAsync(Kask.DAL2.Models.Applicant a);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IApplicantService/UpdateApplicant", ReplyAction="http://tempuri.org/IApplicantService/UpdateApplicantResponse")]
        bool UpdateApplicant(Kask.DAL2.Models.Applicant newApp);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IApplicantService/UpdateApplicant", ReplyAction="http://tempuri.org/IApplicantService/UpdateApplicantResponse")]
        System.Threading.Tasks.Task<bool> UpdateApplicantAsync(Kask.DAL2.Models.Applicant newApp);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IApplicantService/DeleteApplicant", ReplyAction="http://tempuri.org/IApplicantService/DeleteApplicantResponse")]
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
        
        public Kask.DAL2.Models.Applicant GetApplicantByID(int id) {
            return base.Channel.GetApplicantByID(id);
        }
        
        public System.Threading.Tasks.Task<Kask.DAL2.Models.Applicant> GetApplicantByIDAsync(int id) {
            return base.Channel.GetApplicantByIDAsync(id);
        }
        
        public Kask.DAL2.Models.Applicant[] GetApplicants() {
            return base.Channel.GetApplicants();
        }
        
        public System.Threading.Tasks.Task<Kask.DAL2.Models.Applicant[]> GetApplicantsAsync() {
            return base.Channel.GetApplicantsAsync();
        }
        
        public bool CreateApplicant(Kask.DAL2.Models.Applicant a) {
            return base.Channel.CreateApplicant(a);
        }
        
        public System.Threading.Tasks.Task<bool> CreateApplicantAsync(Kask.DAL2.Models.Applicant a) {
            return base.Channel.CreateApplicantAsync(a);
        }
        
        public bool UpdateApplicant(Kask.DAL2.Models.Applicant newApp) {
            return base.Channel.UpdateApplicant(newApp);
        }
        
        public System.Threading.Tasks.Task<bool> UpdateApplicantAsync(Kask.DAL2.Models.Applicant newApp) {
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
        Kask.DAL2.Models.Applied GetAppliedByID(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAppliedService/GetAppliedByID", ReplyAction="http://tempuri.org/IAppliedService/GetAppliedByIDResponse")]
        System.Threading.Tasks.Task<Kask.DAL2.Models.Applied> GetAppliedByIDAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAppliedService/GetApplieds", ReplyAction="http://tempuri.org/IAppliedService/GetAppliedsResponse")]
        Kask.DAL2.Models.Applied[] GetApplieds();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAppliedService/GetApplieds", ReplyAction="http://tempuri.org/IAppliedService/GetAppliedsResponse")]
        System.Threading.Tasks.Task<Kask.DAL2.Models.Applied[]> GetAppliedsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAppliedService/CreateApplied", ReplyAction="http://tempuri.org/IAppliedService/CreateAppliedResponse")]
        bool CreateApplied(Kask.DAL2.Models.Applied a);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAppliedService/CreateApplied", ReplyAction="http://tempuri.org/IAppliedService/CreateAppliedResponse")]
        System.Threading.Tasks.Task<bool> CreateAppliedAsync(Kask.DAL2.Models.Applied a);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAppliedService/UpdateApplied", ReplyAction="http://tempuri.org/IAppliedService/UpdateAppliedResponse")]
        bool UpdateApplied(Kask.DAL2.Models.Applied a);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAppliedService/UpdateApplied", ReplyAction="http://tempuri.org/IAppliedService/UpdateAppliedResponse")]
        System.Threading.Tasks.Task<bool> UpdateAppliedAsync(Kask.DAL2.Models.Applied a);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAppliedService/DeleteApplied", ReplyAction="http://tempuri.org/IAppliedService/DeleteAppliedResponse")]
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
        
        public Kask.DAL2.Models.Applied GetAppliedByID(int id) {
            return base.Channel.GetAppliedByID(id);
        }
        
        public System.Threading.Tasks.Task<Kask.DAL2.Models.Applied> GetAppliedByIDAsync(int id) {
            return base.Channel.GetAppliedByIDAsync(id);
        }
        
        public Kask.DAL2.Models.Applied[] GetApplieds() {
            return base.Channel.GetApplieds();
        }
        
        public System.Threading.Tasks.Task<Kask.DAL2.Models.Applied[]> GetAppliedsAsync() {
            return base.Channel.GetAppliedsAsync();
        }
        
        public bool CreateApplied(Kask.DAL2.Models.Applied a) {
            return base.Channel.CreateApplied(a);
        }
        
        public System.Threading.Tasks.Task<bool> CreateAppliedAsync(Kask.DAL2.Models.Applied a) {
            return base.Channel.CreateAppliedAsync(a);
        }
        
        public bool UpdateApplied(Kask.DAL2.Models.Applied a) {
            return base.Channel.UpdateApplied(a);
        }
        
        public System.Threading.Tasks.Task<bool> UpdateAppliedAsync(Kask.DAL2.Models.Applied a) {
            return base.Channel.UpdateAppliedAsync(a);
        }
        
        public bool DeleteApplied(int id) {
            return base.Channel.DeleteApplied(id);
        }
        
        public System.Threading.Tasks.Task<bool> DeleteAppliedAsync(int id) {
            return base.Channel.DeleteAppliedAsync(id);
        }
    }
}