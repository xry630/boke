import request from '@/utils/request'

export const authApi = {
  login(data) {
    return request.post('/Auth', data)
  },
  
  register(data) {
    return request.post('/Auth/register', data)
  }
}
