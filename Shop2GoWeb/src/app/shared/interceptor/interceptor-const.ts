import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptor } from '../auth/auth.interceptor';
import { UserService } from '../user.service';


export const interceptorProviders = [
    [UserService, { provide: HTTP_INTERCEPTORS , useClass: AuthInterceptor, multi:true}]
]