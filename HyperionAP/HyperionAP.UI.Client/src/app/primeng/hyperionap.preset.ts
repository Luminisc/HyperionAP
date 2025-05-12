import { definePreset } from '@primeng/themes';
import Lara from '@primeng/themes/lara';

export const HyperionAPPreset = definePreset(Lara, {
  components: {
    menubar: {
      padding: '0.5rem 0.5rem',
      border: {
        radius: '0'
      }
    }
  }
});
