export interface CertData {
    subjectName: string;
    issuerName: string;
    validFrom: string;
    validTo: string;
    company: string;
    serialNumber: string;
    certificateStr: string;
    thumbprint: string;
}
