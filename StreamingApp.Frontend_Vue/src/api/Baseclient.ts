export default class BaseClient {
  public transformOptions(options: RequestInit): Promise<RequestInit> {
    // Set headers with users accessToken
    options.headers = new Headers({
      "Content-Type": "application/json; charset=UTF-8",
      "Cache-Control": "no-cache",
      Pragma: "no-cache",
    });
    return Promise.resolve(options);
  }
}
