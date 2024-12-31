export function getAttribute(object, attribute) { return object[attribute]; }

export function isSupported() { return !(!window.PublicKeyCredential); }