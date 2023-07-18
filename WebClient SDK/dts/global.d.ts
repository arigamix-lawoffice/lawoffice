/* eslint-disable no-var */
declare type guid = string;

declare var process: {
  env: {
    [key: string]: string | undefined;
  };
};

declare interface Window {
  __ASPNETCORE_ENVIRONMENT__: string;
  __SERVICE_WORKER_ENABLED__: boolean;
  __AUTHENTICATED__: boolean;
  __WIN_AUTH_PATH__: string;
  __SAML_SIGN_IN__: boolean;
  __BASE_PATH__: string;
  __INSTANCE_PATH__: string;
  __LOGIN_STRINGS__: {
    [key: string]: string;
  };
  __WIN_AUTO_LOGIN__: boolean;

  checkScript: () => void;
  DocsAPI: any;
  safari: any;
  tessa: {
    apiLoader: any;
    assets: Map<string, string>;
    supportedPdfExtensions: string[];
  };
}
// declare namespace Guid {
//   const empty: guid;
//   function equals(a: guid | null | undefined, b: guid | null | undefined): boolean;
//   function newGuid(): guid;
//   function validate(id: guid | null | undefined): boolean;
//   function isEmpty(id: guid | null | undefined): boolean;
// }
