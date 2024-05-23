import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:5100';

export const environment = {
  production: true,
  application: {
    baseUrl,
    name: 'Hackathon',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'http://localhost:5101/',
    redirectUri: baseUrl,
    clientId: 'Hackathon_App',
    responseType: 'code',
    scope: 'offline_access Hackathon',
    requireHttps: false
  },
  apis: {
    default: {
      url: 'http://localhost:5101',
      rootNamespace: 'Hackathon',
    },
  },
} as Environment;
