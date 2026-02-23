<template>
  <div class="ai-dashboard">
    <!-- Animated Background -->
    <div class="ai-bg">
      <div class="ai-bg-orb ai-bg-orb--1"></div>
      <div class="ai-bg-orb ai-bg-orb--2"></div>
      <div class="ai-bg-orb ai-bg-orb--3"></div>
    </div>

    <!-- Hero Section -->
    <section class="hero-section">
      <div class="hero-greeting">
        <div class="ai-icon-wrapper">
          <v-icon size="32" color="white">mdi-robot-happy-outline</v-icon>
        </div>
        <h1 class="hero-title">
          Merhaba, <span class="hero-name">{{ authStore.user?.fullName || 'Kullanıcı' }}</span>
        </h1>
        <p class="hero-subtitle">Size bugün nasıl yardımcı olabilirim?</p>
      </div>

      <!-- AI Search Input -->
      <div class="ai-input-container">
        <div class="ai-input-wrapper" :class="{ 'ai-input-wrapper--focused': isInputFocused }">
          <v-icon class="ai-input-icon" size="22">mdi-magnify</v-icon>
          <input
            v-model="searchQuery"
            type="text"
            class="ai-input"
            placeholder="Bir soru sorun veya makine seçin..."
            @focus="isInputFocused = true"
            @blur="isInputFocused = false"
            @keyup.enter="handleSearch"
          />
          <button 
            v-if="searchQuery" 
            class="ai-input-clear" 
            @click="searchQuery = ''"
          >
            <v-icon size="18">mdi-close-circle</v-icon>
          </button>
          <button class="ai-input-send" @click="handleSearch" :disabled="!searchQuery">
            <v-icon size="20" color="white">mdi-arrow-up</v-icon>
          </button>
        </div>
      </div>
    </section>

    <!-- Two Column Layout -->
    <section class="content-columns">
      <!-- Left: Machine Selection -->
      <div class="panel machine-panel">
        <div class="panel-header">
          <v-icon size="20" class="panel-icon">mdi-cog-outline</v-icon>
          <h2 class="panel-title">Makine Seçimi</h2>
          <span v-if="selectedMachines.length" class="panel-badge">{{ selectedMachines.length }}</span>
        </div>

        <!-- Machine Search -->
        <div class="machine-search">
          <v-icon size="18" class="machine-search-icon">mdi-magnify</v-icon>
          <input
            v-model="machineFilter"
            type="text"
            class="machine-search-input"
            placeholder="Makine ara..."
          />
          <button v-if="machineFilter" class="machine-search-clear" @click="machineFilter = ''">
            <v-icon size="16">mdi-close</v-icon>
          </button>
        </div>

        <!-- Machine List -->
        <div class="machine-list">
          <button
            v-for="machine in filteredMachines"
            :key="machine.id"
            class="machine-item"
            :class="{ 'machine-item--selected': isMachineSelected(machine.id) }"
            @click="toggleMachine(machine)"
          >
            <div class="machine-checkbox">
              <v-icon v-if="isMachineSelected(machine.id)" size="18" color="white">mdi-check</v-icon>
            </div>
            <span class="machine-name">{{ machine.brand }}</span>
          </button>

          <div v-if="!filteredMachines.length" class="machine-empty">
            <v-icon size="28" color="grey-lighten-1">mdi-magnify-close</v-icon>
            <span>Makine bulunamadı</span>
          </div>
        </div>

        <!-- Selected summary -->
        <button 
          v-if="selectedMachines.length > 0" 
          class="machine-clear-btn"
          @click="selectedMachines = []"
        >
          <v-icon size="16">mdi-close-circle-outline</v-icon>
          Seçimi Temizle
        </button>
      </div>

      <!-- Right: Template Questions -->
      <div class="panel templates-panel">
        <div class="panel-header">
          <v-icon size="20" class="panel-icon">mdi-lightbulb-outline</v-icon>
          <h2 class="panel-title">Hazır Şablonlar</h2>
        </div>

        <div class="templates-list">
          <button
            v-for="(template, index) in templateQuestions"
            :key="index"
            class="template-card"
            @click="selectTemplate(template)"
          >
            <div class="template-icon-wrapper" :class="`template-icon--${template.color}`">
              <v-icon size="20" color="white">{{ template.icon }}</v-icon>
            </div>
            <div class="template-content">
              <h3 class="template-title">{{ template.title }}</h3>
              <p class="template-desc">{{ template.description }}</p>
            </div>
            <v-icon class="template-arrow" size="18">mdi-chevron-right</v-icon>
          </button>
        </div>
      </div>
    </section>

    <!-- Footer -->
    <div class="ai-footer">
      <p>MemberShip AI ile daha akıllı üretim yönetimi</p>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed } from 'vue'
import { useAuthStore } from '~/stores/auth'

definePageMeta({
  middleware: ['auth', 'permission'],
})

const authStore = useAuthStore()

const searchQuery = ref('')
const isInputFocused = ref(false)
const machineFilter = ref('')
const selectedMachines = ref<number[]>([])

const templateQuestions = ref([
  {
    icon: 'mdi-alert-circle-outline',
    title: 'Son Arızalar',
    description: 'Son yaşanan arızaları listele ve detaylarını göster',
    color: 'red',
    query: 'Son yaşanan arızaları listele'
  },
  {
    icon: 'mdi-frequently-asked-questions',
    title: 'Arıza Nedeni',
    description: 'Arızanın kök nedenini analiz et ve raporla',
    color: 'amber',
    query: 'Bu arızanın kök nedeni nedir?'
  },
  {
    icon: 'mdi-wrench-outline',
    title: 'Çözüm Önerisi',
    description: 'Arıza için en uygun çözüm yöntemini öner',
    color: 'blue',
    query: 'Bu arıza için çözüm önerisi ver'
  },
  {
    icon: 'mdi-chart-line',
    title: 'Arıza Sıklığı',
    description: 'Tekrarlayan arızaları ve sıklıklarını analiz et',
    color: 'purple',
    query: 'Tekrarlayan arızaları ve sıklıklarını göster'
  },
  {
    icon: 'mdi-clock-alert-outline',
    title: 'Duruş Süresi',
    description: 'Arızadan kaynaklı toplam duruş süresini hesapla',
    color: 'teal',
    query: 'Arızalardan kaynaklı toplam duruş süresini göster'
  },
  {
    icon: 'mdi-shield-alert-outline',
    title: 'Önleyici Bakım',
    description: 'Arıza riskini azaltacak bakım önerilerini listele',
    color: 'green',
    query: 'Arıza riskini azaltmak için önleyici bakım önerilerini listele'
  },
])

const machines = ref([
  { id: 1, brand: 'Siemens' },
  { id: 2, brand: 'Bosch' },
  { id: 3, brand: 'ABB' },
  { id: 4, brand: 'FANUC' },
  { id: 5, brand: 'KUKA' },
  { id: 6, brand: 'Mitsubishi' },
  { id: 7, brand: 'Schneider' },
  { id: 8, brand: 'Haas' },
])

const filteredMachines = computed(() => {
  if (!machineFilter.value) return machines.value
  const q = machineFilter.value.toLowerCase()
  return machines.value.filter(m => m.brand.toLowerCase().includes(q))
})

const isMachineSelected = (id: number) => {
  return selectedMachines.value.includes(id)
}

const toggleMachine = (machine: any) => {
  const idx = selectedMachines.value.indexOf(machine.id)
  if (idx > -1) {
    selectedMachines.value.splice(idx, 1)
  } else {
    selectedMachines.value.push(machine.id)
  }
}

const selectTemplate = (template: any) => {
  searchQuery.value = template.query
}

const handleSearch = () => {
  if (!searchQuery.value.trim()) return
}

useHead({
  title: 'Dashboard - MemberShip AI'
})
</script>

<style scoped>
.ai-dashboard {
  position: relative;
  min-height: 100vh;
  background: #f1f5f9;
  padding: 32px 40px 60px;
  overflow: hidden;
}

/* Animated Background Orbs */
.ai-bg {
  position: fixed;
  inset: 0;
  pointer-events: none;
  z-index: 0;
  overflow: hidden;
}

.ai-bg-orb {
  position: absolute;
  border-radius: 50%;
  filter: blur(120px);
  opacity: 0.12;
  animation: orbFloat 20s ease-in-out infinite;
}

.ai-bg-orb--1 {
  width: 500px;
  height: 500px;
  background: #334155;
  top: -120px;
  right: -100px;
}

.ai-bg-orb--2 {
  width: 400px;
  height: 400px;
  background: #0f172a;
  bottom: -80px;
  left: -60px;
  animation-delay: -7s;
}

.ai-bg-orb--3 {
  width: 300px;
  height: 300px;
  background: #475569;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  animation-delay: -14s;
}

@keyframes orbFloat {
  0%, 100% { transform: translate(0, 0) scale(1); }
  25% { transform: translate(30px, -40px) scale(1.1); }
  50% { transform: translate(-20px, 20px) scale(0.95); }
  75% { transform: translate(15px, 30px) scale(1.05); }
}

/* Hero */
.hero-section {
  position: relative;
  z-index: 1;
  text-align: center;
  padding: 40px 0 36px;
}

.hero-greeting {
  margin-bottom: 32px;
}

.ai-icon-wrapper {
  width: 60px;
  height: 60px;
  margin: 0 auto 18px;
  background: linear-gradient(180deg, #0f172a 0%, #334155 100%);
  border-radius: 18px;
  display: flex;
  align-items: center;
  justify-content: center;
  box-shadow: 0 8px 32px rgba(15, 23, 42, 0.25);
  animation: iconPulse 3s ease-in-out infinite;
}

@keyframes iconPulse {
  0%, 100% { box-shadow: 0 8px 32px rgba(15, 23, 42, 0.25); }
  50% { box-shadow: 0 8px 48px rgba(15, 23, 42, 0.4); }
}

.hero-title {
  font-size: 2.2rem;
  font-weight: 800;
  color: #0f172a;
  letter-spacing: -0.03em;
  margin: 0;
  line-height: 1.2;
}

.hero-name {
  background: linear-gradient(135deg, #0f172a, #475569);
  -webkit-background-clip: text;
  -webkit-text-fill-color: transparent;
  background-clip: text;
}

.hero-subtitle {
  font-size: 1.1rem;
  color: #64748b;
  margin-top: 8px;
  font-weight: 400;
}

/* AI Input */
.ai-input-container {
  max-width: 660px;
  margin: 0 auto;
}

.ai-input-wrapper {
  display: flex;
  align-items: center;
  gap: 12px;
  background: white;
  border: 2px solid #e2e8f0;
  border-radius: 16px;
  padding: 6px 8px 6px 20px;
  transition: all 0.3s cubic-bezier(0.4, 0, 0.2, 1);
  box-shadow: 0 4px 24px rgba(15, 23, 42, 0.04);
}

.ai-input-wrapper--focused {
  border-color: #334155;
  box-shadow: 0 4px 32px rgba(15, 23, 42, 0.1);
}

.ai-input-icon {
  color: #94a3b8;
  flex-shrink: 0;
}

.ai-input {
  flex: 1;
  border: none;
  outline: none;
  font-size: 1rem;
  color: #0f172a;
  background: transparent;
  padding: 12px 0;
  font-family: inherit;
}

.ai-input::placeholder {
  color: #94a3b8;
}

.ai-input-clear {
  background: none;
  border: none;
  cursor: pointer;
  color: #94a3b8;
  padding: 4px;
  display: flex;
  align-items: center;
  transition: color 0.2s;
}

.ai-input-clear:hover {
  color: #475569;
}

.ai-input-send {
  width: 40px;
  height: 40px;
  border-radius: 12px;
  background: linear-gradient(180deg, #0f172a 0%, #334155 100%);
  border: none;
  cursor: pointer;
  display: flex;
  align-items: center;
  justify-content: center;
  flex-shrink: 0;
  transition: all 0.2s;
}

.ai-input-send:hover:not(:disabled) {
  transform: scale(1.05);
  box-shadow: 0 4px 16px rgba(15, 23, 42, 0.3);
}

.ai-input-send:disabled {
  opacity: 0.4;
  cursor: not-allowed;
}

/* Two Column Layout */
.content-columns {
  position: relative;
  z-index: 1;
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 24px;
  margin-bottom: 32px;
}

/* Panel Shared */
.panel {
  background: white;
  border: 1px solid #e2e8f0;
  border-radius: 20px;
  padding: 24px;
  box-shadow: 0 4px 20px rgba(15, 23, 42, 0.04);
}

.panel-header {
  display: flex;
  align-items: center;
  gap: 10px;
  margin-bottom: 18px;
  padding-bottom: 14px;
  border-bottom: 1px solid #f1f5f9;
}

.panel-icon {
  color: #334155;
}

.panel-title {
  font-size: 1.05rem;
  font-weight: 700;
  color: #0f172a;
  margin: 0;
}

.panel-badge {
  margin-left: auto;
  min-width: 24px;
  height: 24px;
  padding: 0 7px;
  border-radius: 12px;
  background: linear-gradient(180deg, #0f172a 0%, #334155 100%);
  color: white;
  font-size: 0.75rem;
  font-weight: 700;
  display: flex;
  align-items: center;
  justify-content: center;
}

/* Machine Panel */
.machine-search {
  display: flex;
  align-items: center;
  gap: 8px;
  background: #f8fafc;
  border: 1px solid #e2e8f0;
  border-radius: 10px;
  padding: 9px 12px;
  margin-bottom: 14px;
  transition: border-color 0.2s;
}

.machine-search:focus-within {
  border-color: #94a3b8;
}

.machine-search-icon {
  color: #94a3b8;
  flex-shrink: 0;
}

.machine-search-input {
  flex: 1;
  border: none;
  outline: none;
  background: transparent;
  font-size: 0.85rem;
  color: #334155;
  font-family: inherit;
}

.machine-search-input::placeholder {
  color: #94a3b8;
}

.machine-search-clear {
  background: none;
  border: none;
  cursor: pointer;
  color: #94a3b8;
  display: flex;
  align-items: center;
  padding: 2px;
  transition: color 0.2s;
}

.machine-search-clear:hover {
  color: #475569;
}

.machine-list {
  display: flex;
  flex-direction: column;
  gap: 2px;
}

.machine-item {
  display: flex;
  align-items: center;
  gap: 12px;
  padding: 11px 14px;
  border-radius: 12px;
  border: 1px solid transparent;
  background: transparent;
  cursor: pointer;
  transition: all 0.2s ease;
  font-family: inherit;
  text-align: left;
}

.machine-item:hover {
  background: #f8fafc;
  border-color: #e2e8f0;
}

.machine-item--selected {
  background: #f0f4ff;
  border-color: #c7d2fe;
}

.machine-item--selected:hover {
  background: #e8edff;
}

.machine-checkbox {
  width: 22px;
  height: 22px;
  border-radius: 6px;
  border: 2px solid #cbd5e1;
  display: flex;
  align-items: center;
  justify-content: center;
  flex-shrink: 0;
  transition: all 0.2s ease;
}

.machine-item--selected .machine-checkbox {
  background: linear-gradient(180deg, #0f172a 0%, #334155 100%);
  border-color: transparent;
  box-shadow: 0 2px 8px rgba(15, 23, 42, 0.2);
}

.machine-name {
  font-size: 0.9rem;
  font-weight: 600;
  color: #334155;
}

.machine-item--selected .machine-name {
  color: #0f172a;
  font-weight: 700;
}

.machine-empty {
  display: flex;
  flex-direction: column;
  align-items: center;
  gap: 8px;
  padding: 32px 0;
  color: #94a3b8;
  font-size: 0.85rem;
}

.machine-clear-btn {
  display: flex;
  align-items: center;
  justify-content: center;
  gap: 6px;
  width: 100%;
  margin-top: 14px;
  padding: 9px;
  border: 1px solid #fecaca;
  border-radius: 10px;
  background: #fff5f5;
  color: #dc2626;
  font-size: 0.8rem;
  font-weight: 600;
  cursor: pointer;
  font-family: inherit;
  transition: all 0.2s;
}

.machine-clear-btn:hover {
  background: #fef2f2;
  border-color: #fca5a5;
}

/* Templates Panel */
.templates-list {
  display: flex;
  flex-direction: column;
  gap: 8px;
}

.template-card {
  display: flex;
  align-items: center;
  gap: 14px;
  padding: 14px 16px;
  background: white;
  border: 1px solid #e2e8f0;
  border-radius: 14px;
  cursor: pointer;
  transition: all 0.25s cubic-bezier(0.4, 0, 0.2, 1);
  text-align: left;
  font-family: inherit;
}

.template-card:hover {
  transform: translateX(4px);
  box-shadow: 0 4px 20px rgba(15, 23, 42, 0.06);
  border-color: #94a3b8;
  background: #f8fafc;
}

.template-icon-wrapper {
  width: 38px;
  height: 38px;
  border-radius: 10px;
  display: flex;
  align-items: center;
  justify-content: center;
  flex-shrink: 0;
}

.template-icon--blue { background: linear-gradient(135deg, #3b82f6, #1d4ed8); }
.template-icon--amber { background: linear-gradient(135deg, #f59e0b, #d97706); }
.template-icon--red { background: linear-gradient(135deg, #ef4444, #dc2626); }
.template-icon--green { background: linear-gradient(135deg, #22c55e, #16a34a); }
.template-icon--purple { background: linear-gradient(135deg, #8b5cf6, #7c3aed); }
.template-icon--teal { background: linear-gradient(135deg, #14b8a6, #0d9488); }

.template-content {
  flex: 1;
  min-width: 0;
}

.template-title {
  font-size: 0.88rem;
  font-weight: 700;
  color: #0f172a;
  margin: 0 0 2px;
}

.template-desc {
  font-size: 0.75rem;
  color: #94a3b8;
  margin: 0;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}

.template-arrow {
  color: #cbd5e1;
  flex-shrink: 0;
  transition: all 0.25s;
}

.template-card:hover .template-arrow {
  color: #334155;
  transform: translateX(3px);
}

/* Footer */
.ai-footer {
  position: relative;
  z-index: 1;
  text-align: center;
  padding-top: 16px;
}

.ai-footer p {
  font-size: 0.8rem;
  color: #94a3b8;
  margin: 0;
}

/* Responsive */
@media (max-width: 960px) {
  .content-columns {
    grid-template-columns: 1fr;
  }

}

@media (max-width: 768px) {
  .ai-dashboard {
    padding: 20px 16px 40px;
  }

  .hero-title {
    font-size: 1.8rem;
  }
}

</style>
