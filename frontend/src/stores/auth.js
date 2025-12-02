import { defineStore } from 'pinia'
import { ref, computed } from 'vue'

export const useAuthStore = defineStore('auth', () => {
  const token = ref(localStorage.getItem('token') || '')
  const userInfo = ref(JSON.parse(localStorage.getItem('userInfo') || 'null'))

  const isAuthenticated = computed(() => !!token.value)

  function setToken(newToken) {
    console.log('Setting token:', newToken, 'Type:', typeof newToken)
    token.value = newToken
    if (newToken) {
      localStorage.setItem('token', newToken)
      parseToken(newToken)
    } else {
      localStorage.removeItem('token')
    }
  }

  function parseToken(jwtToken) {
    try {
      // Ensure jwtToken is a string
      if (typeof jwtToken !== 'string') {
        console.error('Token is not a string:', jwtToken)
        return
      }
      
      const base64Url = jwtToken.split('.')[1]
      const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/')
      const jsonPayload = decodeURIComponent(
        atob(base64)
          .split('')
          .map((c) => '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2))
          .join('')
      )
      const payload = JSON.parse(jsonPayload)
      
      if (payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/issuer']) {
        const userData = JSON.parse(payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/issuer'])
        setUserInfo(userData)
      }
    } catch (error) {
      console.error('解析token失败:', error)
    }
  }

  function setUserInfo(info) {
    userInfo.value = info
    if (info) {
      localStorage.setItem('userInfo', JSON.stringify(info))
    } else {
      localStorage.removeItem('userInfo')
    }
  }

  function logout() {
    token.value = ''
    userInfo.value = null
    localStorage.removeItem('token')
    localStorage.removeItem('userInfo')
  }

  return {
    token,
    userInfo,
    isAuthenticated,
    setToken,
    setUserInfo,
    logout
  }
})
