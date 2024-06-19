//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v14.0.7.0 (NJsonSchema v11.0.0.0 (Newtonsoft.Json v13.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------

/* tslint:disable */
/* eslint-disable */
// ReSharper disable InconsistentNaming

import {
  HttpClient,
  HttpHeaders,
  HttpResponse,
  HttpResponseBase,
} from '@angular/common/http';
import { Inject, Injectable, InjectionToken, Optional } from '@angular/core';
import {
  Observable,
  of as _observableOf,
  throwError as _observableThrow,
} from 'rxjs';
import {
  catchError as _observableCatch,
  mergeMap as _observableMergeMap,
} from 'rxjs/operators';

export const API_BASE_URL = new InjectionToken<string>('API_BASE_URL');

@Injectable({
  providedIn: 'root',
})
export class CommandClient {
  private http: HttpClient;
  private baseUrl: string;
  protected jsonParseReviver: ((key: string, value: any) => any) | undefined =
    undefined;

  constructor(
    @Inject(HttpClient) http: HttpClient,
    @Optional() @Inject(API_BASE_URL) baseUrl?: string
  ) {
    this.http = http;
    this.baseUrl = baseUrl ?? '';
  }

  getAllCommands(): Observable<CommandRespose> {
    let url_ = this.baseUrl + '/api/Command';
    url_ = url_.replace(/[?&]$/, '');

    let options_: any = {
      observe: 'response',
      responseType: 'blob',
      headers: new HttpHeaders({
        Accept: 'application/json',
      }),
    };

    return this.http
      .request('get', url_, options_)
      .pipe(
        _observableMergeMap((response_: any) => {
          return this.processGetAllCommands(response_);
        })
      )
      .pipe(
        _observableCatch((response_: any) => {
          if (response_ instanceof HttpResponseBase) {
            try {
              return this.processGetAllCommands(response_ as any);
            } catch (e) {
              return _observableThrow(e) as any as Observable<CommandRespose>;
            }
          } else
            return _observableThrow(
              response_
            ) as any as Observable<CommandRespose>;
        })
      );
  }

  protected processGetAllCommands(
    response: HttpResponseBase
  ): Observable<CommandRespose> {
    const status = response.status;
    const responseBlob =
      response instanceof HttpResponse
        ? response.body
        : (response as any).error instanceof Blob
        ? (response as any).error
        : undefined;

    let _headers: any = {};
    if (response.headers) {
      for (let key of response.headers.keys()) {
        _headers[key] = response.headers.get(key);
      }
    }
    if (status === 200) {
      return blobToText(responseBlob).pipe(
        _observableMergeMap((_responseText: string) => {
          let result200: any = null;
          let resultData200 =
            _responseText === ''
              ? null
              : JSON.parse(_responseText, this.jsonParseReviver);
          result200 = CommandRespose.fromJS(resultData200);
          return _observableOf(result200);
        })
      );
    } else if (status !== 200 && status !== 204) {
      return blobToText(responseBlob).pipe(
        _observableMergeMap((_responseText: string) => {
          return throwException(
            'An unexpected server error occurred.',
            status,
            _responseText,
            _headers
          );
        })
      );
    }
    return _observableOf(null as any);
  }

  updateCommands(
    commandAndResponses: CommandAndResponseDto[]
  ): Observable<CommandRespose> {
    let url_ = this.baseUrl + '/api/Command';
    url_ = url_.replace(/[?&]$/, '');

    const content_ = JSON.stringify(commandAndResponses);

    let options_: any = {
      body: content_,
      observe: 'response',
      responseType: 'blob',
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Accept: 'application/json',
      }),
    };

    return this.http
      .request('post', url_, options_)
      .pipe(
        _observableMergeMap((response_: any) => {
          return this.processUpdateCommands(response_);
        })
      )
      .pipe(
        _observableCatch((response_: any) => {
          if (response_ instanceof HttpResponseBase) {
            try {
              return this.processUpdateCommands(response_ as any);
            } catch (e) {
              return _observableThrow(e) as any as Observable<CommandRespose>;
            }
          } else
            return _observableThrow(
              response_
            ) as any as Observable<CommandRespose>;
        })
      );
  }

  protected processUpdateCommands(
    response: HttpResponseBase
  ): Observable<CommandRespose> {
    const status = response.status;
    const responseBlob =
      response instanceof HttpResponse
        ? response.body
        : (response as any).error instanceof Blob
        ? (response as any).error
        : undefined;

    let _headers: any = {};
    if (response.headers) {
      for (let key of response.headers.keys()) {
        _headers[key] = response.headers.get(key);
      }
    }
    if (status === 200) {
      return blobToText(responseBlob).pipe(
        _observableMergeMap((_responseText: string) => {
          let result200: any = null;
          let resultData200 =
            _responseText === ''
              ? null
              : JSON.parse(_responseText, this.jsonParseReviver);
          result200 = CommandRespose.fromJS(resultData200);
          return _observableOf(result200);
        })
      );
    } else if (status !== 200 && status !== 204) {
      return blobToText(responseBlob).pipe(
        _observableMergeMap((_responseText: string) => {
          return throwException(
            'An unexpected server error occurred.',
            status,
            _responseText,
            _headers
          );
        })
      );
    }
    return _observableOf(null as any);
  }
}

@Injectable({
  providedIn: 'root',
})
export class TwitchClient {
  private http: HttpClient;
  private baseUrl: string;
  protected jsonParseReviver: ((key: string, value: any) => any) | undefined =
    undefined;

  constructor(
    @Inject(HttpClient) http: HttpClient,
    @Optional() @Inject(API_BASE_URL) baseUrl?: string
  ) {
    this.http = http;
    this.baseUrl = baseUrl ?? '';
  }

  startTwitchRequest(): Observable<void> {
    let url_ = this.baseUrl + '/api/Twitch';
    url_ = url_.replace(/[?&]$/, '');

    let options_: any = {
      observe: 'response',
      responseType: 'blob',
      headers: new HttpHeaders({}),
    };

    return this.http
      .request('put', url_, options_)
      .pipe(
        _observableMergeMap((response_: any) => {
          return this.processStartTwitchRequest(response_);
        })
      )
      .pipe(
        _observableCatch((response_: any) => {
          if (response_ instanceof HttpResponseBase) {
            try {
              return this.processStartTwitchRequest(response_ as any);
            } catch (e) {
              return _observableThrow(e) as any as Observable<void>;
            }
          } else return _observableThrow(response_) as any as Observable<void>;
        })
      );
  }

  protected processStartTwitchRequest(
    response: HttpResponseBase
  ): Observable<void> {
    const status = response.status;
    const responseBlob =
      response instanceof HttpResponse
        ? response.body
        : (response as any).error instanceof Blob
        ? (response as any).error
        : undefined;

    let _headers: any = {};
    if (response.headers) {
      for (let key of response.headers.keys()) {
        _headers[key] = response.headers.get(key);
      }
    }
    if (status === 200) {
      return blobToText(responseBlob).pipe(
        _observableMergeMap((_responseText: string) => {
          return _observableOf(null as any);
        })
      );
    } else if (status !== 200 && status !== 204) {
      return blobToText(responseBlob).pipe(
        _observableMergeMap((_responseText: string) => {
          return throwException(
            'An unexpected server error occurred.',
            status,
            _responseText,
            _headers
          );
        })
      );
    }
    return _observableOf(null as any);
  }

  getTwitchChatData(): Observable<void> {
    let url_ = this.baseUrl + '/api/Twitch';
    url_ = url_.replace(/[?&]$/, '');

    let options_: any = {
      observe: 'response',
      responseType: 'blob',
      headers: new HttpHeaders({}),
    };

    return this.http
      .request('get', url_, options_)
      .pipe(
        _observableMergeMap((response_: any) => {
          return this.processGetTwitchChatData(response_);
        })
      )
      .pipe(
        _observableCatch((response_: any) => {
          if (response_ instanceof HttpResponseBase) {
            try {
              return this.processGetTwitchChatData(response_ as any);
            } catch (e) {
              return _observableThrow(e) as any as Observable<void>;
            }
          } else return _observableThrow(response_) as any as Observable<void>;
        })
      );
  }

  protected processGetTwitchChatData(
    response: HttpResponseBase
  ): Observable<void> {
    const status = response.status;
    const responseBlob =
      response instanceof HttpResponse
        ? response.body
        : (response as any).error instanceof Blob
        ? (response as any).error
        : undefined;

    let _headers: any = {};
    if (response.headers) {
      for (let key of response.headers.keys()) {
        _headers[key] = response.headers.get(key);
      }
    }
    if (status === 200) {
      return blobToText(responseBlob).pipe(
        _observableMergeMap((_responseText: string) => {
          return _observableOf(null as any);
        })
      );
    } else if (status !== 200 && status !== 204) {
      return blobToText(responseBlob).pipe(
        _observableMergeMap((_responseText: string) => {
          return throwException(
            'An unexpected server error occurred.',
            status,
            _responseText,
            _headers
          );
        })
      );
    }
    return _observableOf(null as any);
  }
}

export class CommandRespose implements ICommandRespose {
  cads!: CommandAndResponseDto[];
  isSucsess!: boolean;

  constructor(data?: ICommandRespose) {
    if (data) {
      for (var property in data) {
        if (data.hasOwnProperty(property))
          (<any>this)[property] = (<any>data)[property];
      }
    }
    if (!data) {
      this.cads = [];
    }
  }

  init(_data?: any) {
    if (_data) {
      if (Array.isArray(_data['cads'])) {
        this.cads = [] as any;
        for (let item of _data['cads'])
          this.cads!.push(CommandAndResponseDto.fromJS(item));
      } else {
        this.cads = <any>null;
      }
      this.isSucsess =
        _data['isSucsess'] !== undefined ? _data['isSucsess'] : <any>null;
    }
  }

  static fromJS(data: any): CommandRespose {
    data = typeof data === 'object' ? data : {};
    let result = new CommandRespose();
    result.init(data);
    return result;
  }

  toJSON(data?: any) {
    data = typeof data === 'object' ? data : {};
    if (Array.isArray(this.cads)) {
      data['cads'] = [];
      for (let item of this.cads) data['cads'].push(item.toJSON());
    }
    data['isSucsess'] =
      this.isSucsess !== undefined ? this.isSucsess : <any>null;
    return data;
  }
}

export interface ICommandRespose {
  cads: CommandAndResponseDto[];
  isSucsess: boolean;
}

export class CommandAndResponseDto implements ICommandAndResponseDto {
  id!: number;
  command!: string;
  response!: string;
  description!: string;
  active!: boolean;
  auth!: AuthEnum;
  category!: CategoryEnum;

  constructor(data?: ICommandAndResponseDto) {
    if (data) {
      for (var property in data) {
        if (data.hasOwnProperty(property))
          (<any>this)[property] = (<any>data)[property];
      }
    }
  }

  init(_data?: any) {
    if (_data) {
      this.id = _data['id'] !== undefined ? _data['id'] : <any>null;
      this.command =
        _data['command'] !== undefined ? _data['command'] : <any>null;
      this.response =
        _data['response'] !== undefined ? _data['response'] : <any>null;
      this.description =
        _data['description'] !== undefined ? _data['description'] : <any>null;
      this.active = _data['active'] !== undefined ? _data['active'] : <any>null;
      this.auth = _data['auth'] !== undefined ? _data['auth'] : <any>null;
      this.category =
        _data['category'] !== undefined ? _data['category'] : <any>null;
    }
  }

  static fromJS(data: any): CommandAndResponseDto {
    data = typeof data === 'object' ? data : {};
    let result = new CommandAndResponseDto();
    result.init(data);
    return result;
  }

  toJSON(data?: any) {
    data = typeof data === 'object' ? data : {};
    data['id'] = this.id !== undefined ? this.id : <any>null;
    data['command'] = this.command !== undefined ? this.command : <any>null;
    data['response'] = this.response !== undefined ? this.response : <any>null;
    data['description'] =
      this.description !== undefined ? this.description : <any>null;
    data['active'] = this.active !== undefined ? this.active : <any>null;
    data['auth'] = this.auth !== undefined ? this.auth : <any>null;
    data['category'] = this.category !== undefined ? this.category : <any>null;
    return data;
  }
}

export interface ICommandAndResponseDto {
  id: number;
  command: string;
  response: string;
  description: string;
  active: boolean;
  auth: AuthEnum;
  category: CategoryEnum;
}

export enum AuthEnum {
  Streamer = 0,
  Mod = 1,
  Staff = 2,
  Vip = 3,
  Subscriber = 4,
  Turbo = 5,
  Partner = 6,
  Undefined = 7,
}

export enum CategoryEnum {
  Undefined = 0,
  Queue = 1,
  Game = 2,
  Song = 3,
  Streamupdate = 4,
  Fun = 5,
  Subathon = 6,
}

export class ApiException extends Error {
  override message: string;
  status: number;
  response: string;
  headers: { [key: string]: any };
  result: any;

  constructor(
    message: string,
    status: number,
    response: string,
    headers: { [key: string]: any },
    result: any
  ) {
    super();

    this.message = message;
    this.status = status;
    this.response = response;
    this.headers = headers;
    this.result = result;
  }

  protected isApiException = true;

  static isApiException(obj: any): obj is ApiException {
    return obj.isApiException === true;
  }
}

function throwException(
  message: string,
  status: number,
  response: string,
  headers: { [key: string]: any },
  result?: any
): Observable<any> {
  if (result !== null && result !== undefined) return _observableThrow(result);
  else
    return _observableThrow(
      new ApiException(message, status, response, headers, null)
    );
}

function blobToText(blob: any): Observable<string> {
  return new Observable<string>((observer: any) => {
    if (!blob) {
      observer.next('');
      observer.complete();
    } else {
      let reader = new FileReader();
      reader.onload = (event) => {
        observer.next((event.target as any).result);
        observer.complete();
      };
      reader.readAsText(blob);
    }
  });
}
