import request from '@/utils/request'

export const codeApi = {
  getCode(params) {
    return request.get('/Code', { params })
  }
}
