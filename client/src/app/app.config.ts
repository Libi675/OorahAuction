import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';


import { routes } from './app.routes';
import { provideHttpClient } from '@angular/common/http';
import { provideRouter, withHashLocation } from '@angular/router';

export const appConfig: ApplicationConfig = {
//providers: [provideZoneChangeDetection({ eventCoalescing: true }), provideRouter(routes), provideClientHydration()]
  //providers: [provideRouter(routes), provideHttpClient(),provideZoneChangeDetection({ eventCoalescing: true }), provideClientHydration()]
  providers: [provideZoneChangeDetection({ eventCoalescing: true }),provideRouter(routes, withHashLocation()), provideHttpClient()]
  
};
