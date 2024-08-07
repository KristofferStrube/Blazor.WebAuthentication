# Blazor.WebAuthentication
A Blazor wrapper for the [Web Authentication](https://www.w3.org/TR/webauthn-3/) browser API.

The API specifies ways to create and validate strong public-key-based credentials. It gets these credentials from the native authenticators of the device. On Windows, that's Windows Hello; on iOS/macOS, that's Touch ID or Face ID; and on Android, that's face, fingerprint, or PIN authentication. This project implements a wrapper around the API for Blazor so that we can easily and safely work with native authentication methods from the browser.

*This wrapper is still under development.*

# Demo
The sample project can be demoed at https://kristofferstrube.github.io/Blazor.WebAuthentication/

On each page, you can find the corresponding code for the example in the top right corner.

## Logging and monitoring
For the demo, I use [elmah.io](https://elmah.io) for logging and monitoring. This helps me to debug errors that might occur on specific devices or under special circumstances. The use of Error Handling JSInterop from [Blazor.WebIDL](https://github.com/KristofferStrube/Blazor.WebIDL) combined with elmah.io makes this especially useful.


elmah.io gives a free Small Business subscription to any OSS project. Read more about this here: [Open Source - Monitor your open source website for free](https://elmah.io/sponsorship/opensource/)

# Related articles
This repository was built with inspiration and help from the following series of articles:

- [Typed exceptions for JSInterop in Blazor](https://kristoffer-strube.dk/post/typed-exceptions-for-jsinterop-in-blazor/)
- [Wrapping JavaScript libraries in Blazor WebAssembly/WASM](https://blog.elmah.io/wrapping-javascript-libraries-in-blazor-webassembly-wasm/)
- [Call anonymous C# functions from JS in Blazor WASM](https://blog.elmah.io/call-anonymous-c-functions-from-js-in-blazor-wasm/)
- [Using JS Object References in Blazor WASM to wrap JS libraries](https://blog.elmah.io/using-js-object-references-in-blazor-wasm-to-wrap-js-libraries/)
- [Blazor WASM 404 error and fix for GitHub Pages](https://blog.elmah.io/blazor-wasm-404-error-and-fix-for-github-pages/)
- [How to fix Blazor WASM base path problems](https://blog.elmah.io/how-to-fix-blazor-wasm-base-path-problems/)
