# ParseSPL
## About
Welcome to the repository for the Service Provider List Parser application! This project utilizes PdfPig, a PDF document parsing library in C#, to extract a list of services from Service Provider List PDF documents. The tool is designed to streamline the process of retrieving service information from these documents, providing users with a structured list of services available within the document.
## Usage
To run this application, just use the following command through a CLI: `ParseSPL.exe <filename or directory>`
## Key Features
- **Date Issued Extraction:** Extracts the date the Service Provider List was issued on.
- **Service Identification:** Accurately identifies and delivers a list of services mentioned in the document.
- **Cross Platform Compatibility:** Ensures the application functions on both Windows and Linux environments.
- **Error Checking:** Implements robust error-checking mechanisms to validate and prevent the use of faulty or corrupted PDF documents.
## Limitations
- **Handling Multiple Services from a Single Provider:** The application does not effectively handle cases where multiple services are provided by a single service provider. This can cause an exception to be thrown and the Service Provider List not to be parsed.
- **Inconsistent Error Checking:** Due to the variance of PDFs, some PDFs may slip past the error checking and the application may be able to successfully parse the PDF document even if it contains errors.
- **Limited Date Extraction:** As of right now, there is only a limited selection of date formats that will be recognized by the application. This selection is planned to be expanded in the future.
## Data Pipeline
![Data Pipeline](https://github.com/basicn86/ParseSPL/blob/master/screenshots/datapipeline.png)
## Example
![Example 1](https://github.com/basicn86/ParseSPL/blob/master/screenshots/ex1.png)
