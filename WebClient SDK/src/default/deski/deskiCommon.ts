const BASE_PATH = window.__BASE_PATH__;
export const APP_URL = BASE_PATH
  ? `${window.location.origin}/${BASE_PATH}`
  : window.location.origin;
