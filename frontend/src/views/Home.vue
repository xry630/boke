<template>
  <div class="home-container">
    <div class="home-card">
      <h1 class="home-title">视频管理系统</h1>
      <div v-if="authStore.userInfo" class="home-user">
        <p>欢迎，{{ authStore.userInfo.name || authStore.userInfo.username }}！</p>
        <p style="margin-top: 10px; font-size: 14px; color: #999;">
          用户ID: {{ authStore.userInfo.id }}
        </p>
        <p v-if="authStore.userInfo.role && authStore.userInfo.role.length" 
           style="margin-top: 10px; font-size: 14px; color: #999;">
          角色: {{ authStore.userInfo.role.map(r => r.name).join(', ') }}
        </p>
      </div>
      <div v-else class="home-user">
        <p>您已成功登录！</p>
      </div>
      <button class="btn-logout" @click="handleLogout">退出登录</button>
    </div>
  </div>
</template>

<script setup>
import { useRouter } from 'vue-router'
import { useAuthStore } from '@/stores/auth'

const router = useRouter()
const authStore = useAuthStore()

const handleLogout = () => {
  authStore.logout()
  router.push('/login')
}
</script>
